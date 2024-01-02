using Microsoft.AspNetCore.Http;
using System.Net;
namespace VirtualPetCareAPI.Service.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = System.Text.Json.JsonSerializer.Serialize(new 
            {
                 error = ex.Message,
            });

            await httpContext.Response.WriteAsync(result);
        }
    }
}