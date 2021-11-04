using System;
using System.Net;
using JustTradeIt.Software.API.Models;
using JustTradeIt.Software.API.Models.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace JustTradeIt.Software.API.ExceptionHandlerExtensions
{
    public static class ExceptionHandlerExtensions
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var excetionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    
                    if (excetionHandlerFeature != null)
                    {
                        var exception = excetionHandlerFeature.Error;
                        var statusCode = (int) HttpStatusCode.InternalServerError;
                        
                        
                        if (exception is ArgumentOutOfRangeException)
                        {
                            
                            statusCode = (int) HttpStatusCode.BadRequest;
                        }
                        else if (exception is ResourceNotFoundException)
                        {
                            statusCode = (int) HttpStatusCode.NotFound;
                            
                        }
                        else if (exception is NullReferenceException)
                        {
                            statusCode = (int) HttpStatusCode.BadRequest;
                        }
                        else if (exception is ModelFormatException)
                        {
                            statusCode = (int) HttpStatusCode.BadRequest;
                            
                        }
                        else if (exception is ArgumentNullException)
                        {
                            statusCode = (int) HttpStatusCode.BadRequest;
                        }
                        
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = statusCode;

                        

                            await context.Response.WriteAsync(new ExceptionModel
                        {
                            StatusCode = statusCode,
                            ExceptionMessage = exception.Message,
                            
                            
                        }.ToString());

                    }
                });
            });
        }
    }
}