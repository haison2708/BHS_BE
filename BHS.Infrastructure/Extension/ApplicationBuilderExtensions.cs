using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BHS.Infrastructure.Extension;

public static class ApplicationBuilderExtensions
{
    public static void UseFluentValidationExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(x =>
        {
            x.Run(async context =>
            {
                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = errorFeature!.Error;
                if (exception is not ValidationException validationException)
                    throw exception;
                var error =
                    validationException.Errors.Select(err => new
                        { err.ErrorCode, err.PropertyName, err.ErrorMessage }).FirstOrDefault();
                var errorText = JsonConvert.SerializeObject(error);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(errorText, Encoding.UTF8);
            });
        });
    }
}