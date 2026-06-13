using Serilog;
using System;
using System.Net;

using System.Text.Json;

namespace StudentManagement.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unhandled exception occurred");
            context.Response.ContentType ="application/json";
            context.Response.StatusCode =(int)HttpStatusCode.InternalServerError;
            var response = new
            {
                message = "An unexpected error occurred"
            };
            var jsonResponse =JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}