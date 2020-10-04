using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

public static class CustomErrorHandlerHelper{
    public static void UseCustomErrors(this IApplicationBuilder application, IHostEnvironment environment){
        if (environment.IsDevelopment()) {
            application.Use(WriteDevelopmentResponse);
        }
        else {
            application.Use(WriteProductResponse);
        }
    }

    private static Task WriteDevelopmentResponse(HttpContext httpContext, Func<Task> next)
        => WriteResponse(httpContext, includeDetails: true);

    private static Task WriteProductResponse(HttpContext httpContext, Func<Task> next)
        => WriteResponse(httpContext, includeDetails: false);

    private static async Task WriteResponse(HttpContext httpContext, bool includeDetails){
        var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();    
        var ex = exceptionDetails?.Error;

        if(ex != null) {
            httpContext.Response.ContentType = "application/problem+json";

            var title = includeDetails ? "An error has occured: " + ex.Message : "An error has occured";
            var details = includeDetails ? ex.ToString() : null;

            var problem = new ProblemDetails
            {
                Status = 500,
                Title = title,
                Detail = details
            };

            if(ex is NotFoundException) problem.Status = 404;
            else if(ex is WrongItemTypeException) problem.Status = 400;
            
            var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
            if(traceId != null) {
                problem.Extensions["traceId"] = traceId;
            }

            var stream = httpContext.Response.Body;
            await JsonSerializer.SerializeAsync(stream, problem);
        }
    }

}