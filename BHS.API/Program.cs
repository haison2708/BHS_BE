using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Web;
using BHS.API.PipelineBehaviors;
using BHS.API.Services;
using BHS.Domain.SeedWork;
using BHS.Infrastructure;
using BHS.Infrastructure.Extension;
using BHS.Infrastructure.Filters;
using Azure.Storage.Blobs;
using FluentValidation;
using Hangfire;
using Hangfire.SqlServer;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;

[assembly: RootNamespace("BHS.API")]

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());

// Add services to the container.
builder.Services.AddSignalR(x => x.EnableDetailedErrors = true);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        x => x.SetIsOriginAllowed(host => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

#region AddSwagger

builder.Services.AddEndpointsApiExplorer();
var version = Assembly.GetEntryAssembly()!.GetName().Version;
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "eTrade - Catalog HTTP API",
        Version = "v1",
        Description = $"The Catalog Service HTTP API - Date Build {version}"
    });
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri($"{builder.Configuration["identityUrl"]}/connect/authorize"),
                TokenUrl = new Uri($"{builder.Configuration["identityUrlExternal"]}/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    {"api", "api"}
                }
            }
        }
    });
    options.OperationFilter<AuthorizeCheckOperationFilter>();
    options.OperationFilter<AddHeaderParameterOperationFilter>();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath)) options.IncludeXmlComments(xmlPath);
});

#endregion

#region Inject Services

var serviceTypes = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(x => x.GetTypes())
    .Where(x => (typeof(ITransientService).IsAssignableFrom(x) || typeof(IScopedService).IsAssignableFrom(x)
                                                               || typeof(ISingletonService).IsAssignableFrom(x))
                && x != typeof(ITransientService) && x != typeof(IScopedService) &&
                x != typeof(ISingletonService) && x != typeof(IQuery) && x.IsInterface).ToList();
foreach (var serviceType in serviceTypes)
{
    var implementationType = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(x => x.GetTypes())
        .FirstOrDefault(x => serviceType.IsAssignableFrom(x) && x.IsClass);
    if (implementationType is null || implementationType == serviceType) continue;
    if (typeof(ITransientService).IsAssignableFrom(serviceType))
        builder.Services.AddTransient(serviceType, implementationType);
    else if (typeof(IScopedService).IsAssignableFrom(serviceType))
        builder.Services.AddScoped(serviceType, implementationType);
    else if (typeof(ISingletonService).IsAssignableFrom(serviceType))
        builder.Services.AddSingleton(serviceType, implementationType);
    else
        builder.Services.AddScoped(serviceType, implementationType);
}

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

//Add Mediator
builder.Services.AddMediatR(typeof(Program).Assembly);

builder.Services.AddDbContextPool<BHSDbContext>(x =>
    x.UseSqlServer(builder.Configuration["ConnectionString"]));

builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = builder.Configuration["RedisUrl"]; });

//Add AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Add FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

//Add Azure
builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration["BlobStorageConnectionString"]));

//Add Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

//Add Hangfire
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseSerializerSettings(new JsonSerializerSettings
    {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    })
    .UseSqlServerStorage(builder.Configuration["ConnectionString"], new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    }));
builder.Services.AddHangfireServer();

#endregion

#region Config Authenticaton

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
var identityUrl = builder.Configuration["identityUrl"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddIdentityServerAuthentication(options =>
{
    options.Authority = identityUrl;
    options.ApiName = "api";
    options.ApiSecret = "Secret";
    options.RequireHttpsMetadata = false;
});

#endregion

var app = builder.Build();

app.UseSerilogRequestLogging(configure =>
{
    configure.MessageTemplate =
        "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000}ms";
});

#region PathBase

var pathBase = builder.Configuration["PATH_BASE"];

if (!string.IsNullOrEmpty(pathBase)) app.UsePathBase(pathBase);

#endregion

#region Middleware

app.Use(async (context, next) =>
{
    if (context.Request.QueryString.HasValue)
        if (string.IsNullOrWhiteSpace(context.Request.Headers["Authorization"]))
        {
            var queryString = HttpUtility.ParseQueryString(context.Request.QueryString.Value!);
            var token = queryString.Get("access_token");

            if (!string.IsNullOrWhiteSpace(token))
                context.Request.Headers.Add("Authorization", new[] {$"Bearer {token}"});
        }

    await next.Invoke();
});

#endregion

#region Use Swagger

app.UseSwagger(
        c =>
        {
            c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            {
                if (app.Environment.IsDevelopment())
                    swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new() {Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{pathBase}"},
                        new() {Url = $"https://{httpReq.Host.Value}{pathBase}"}
                    };
                else
                    swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new() {Url = $"https://{httpReq.Host.Value}{pathBase}"},
                        new() {Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{pathBase}"}
                    };
            });
        }
    )
    .UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"{(!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty)}/swagger/v1/swagger.json",
            "Catalog.API V1.01");
        c.OAuthClientId("ro.client");
        c.OAuthClientSecret("secret");
        c.OAuthAppName("Catalog Swagger UI");
    });

#endregion

#region Localization

var supportedCultures = new[] {"en", "vi"};
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

localizationOptions.ApplyCurrentCultureToResponseHeaders = true;
localizationOptions.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
{
    var userLang = context.Request.Headers["LangID"].ToString();
    var firstLang = userLang.Split(',').FirstOrDefault();
    var defaultLang = string.IsNullOrEmpty(firstLang) ? "vi" : firstLang;
    return Task.FromResult(new ProviderCultureResult(defaultLang, defaultLang))!;
}));

app.UseRequestLocalization(localizationOptions);

#endregion

if (app.Environment.IsDevelopment()) app.UseHangfireDashboard();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();
app.UseFluentValidationExceptionHandler();
app.UseEndpoints(endpoints => { endpoints.MapHub<NotificationHub>("/notification"); });

app.MapDefaultControllerRoute();
app.MapControllers();

app.Run();