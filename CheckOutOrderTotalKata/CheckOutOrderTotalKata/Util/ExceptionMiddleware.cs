using CheckOutOrderTotalKata.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace CheckOutOrderTotalKata.Util
{
    /// <summary>
    /// Exception Middleware
    /// </summary>
    public static class ExceptionMiddleware
    {
        /// <summary>
        /// Middleware Exception handler to return 500 result when unknown error occurs
        /// </summary>
        /// <param name="app">The application.</param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
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
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                    
                });
            });
        }
    }
}
