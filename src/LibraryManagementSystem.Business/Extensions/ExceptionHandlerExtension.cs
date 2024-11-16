using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace LibraryManagementSystem.Business.Extensions;

public static class ExceptionHandlerExtension
{
    public static IApplicationBuilder AddExceptionHandlerService(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(error =>
        {
            error.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                int statusCode = (int)HttpStatusCode.InternalServerError;
                //string message = "Internal Server Error";
                string message = contextFeature.Error.Message;

                if (contextFeature != null)
                {

                    if (contextFeature.Error is IBaseException)
                    {
                        var exception = (IBaseException)contextFeature.Error;
                        statusCode = exception.StatusCode;
                        message = exception.Message;
                    }
                }

                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsJsonAsync(new GenericResponseDto(statusCode, message));
                await context.Response.CompleteAsync();
            });
        });

        return app;
    }
}
