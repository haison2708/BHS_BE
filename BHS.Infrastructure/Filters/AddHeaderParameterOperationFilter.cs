﻿using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BHS.Infrastructure.Filters;

public class AddHeaderParameterOperationFilter : IOperationFilter
{
    private readonly IServiceProvider _serviceProvider;

    public AddHeaderParameterOperationFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();


        /*var enumList = (_serviceProvider.GetService(typeof(IOptions<RequestLocalizationOptions>)) as IOptions<RequestLocalizationOptions>)?
                    .Value?.SupportedCultures?.Select(c => OpenApiAnyFactory.CreateFor(new OpenApiSchema() { Type = "string" }, c.Name)).ToList();*/

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "LangID",
            In = ParameterLocation.Header,
            Description = "Default language= vi",
            Schema = new OpenApiSchema
            {
                Default = new OpenApiString("vi"),
                Type = "string"
            }
        });

        /*operation.Parameters.Add(new OpenApiParameter()
        {
            Name = "TimeZone",
            In = ParameterLocation.Header,
            Schema = new OpenApiSchema
            {
                Default = new OpenApiString("SE Asia Standard Time"),
                Type = "string"
            },
        });*/
    }
}