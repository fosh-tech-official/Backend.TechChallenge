using System;
using System.Net;
using Backend.TechChallenge.Api.Exceptions;
using Backend.TechChallenge.Api.LoggerService;
using Backend.TechChallenge.Api.Middleware.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Backend.TechChallenge.Api.Middleware
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var errorStatusCode = GetErrorStatusCode(contextFeature.Error);
                        context.Response.StatusCode = errorStatusCode;

                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = errorStatusCode,
                            Message = $"{contextFeature.Error.Message}"
                        }.ToString());
                    }
                });
            });
        }

        private static int GetErrorStatusCode(Exception exception)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;
            if (exception is InvalidRequestException ex) statusCode = ex.StatusCode;
            return statusCode;
        }
    }
}
