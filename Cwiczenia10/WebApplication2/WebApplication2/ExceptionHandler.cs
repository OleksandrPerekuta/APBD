using System.Text.Json;
using WebApplication2.Exception;

namespace WebApplication2;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;

    public ExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (EntityNotFoundException ex)
        {
            await HandleExceptionAsync(context, ex, 404);
        }
        catch (System.Exception ex)
        {
            await HandleExceptionAsync(context, ex, 500);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, System.Exception ex, int statusCode)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
            
        var jsonResponse = JsonSerializer.Serialize(new
        {
            Message = ex.Message,
            Time = DateTime.Now
        });
            
        await context.Response.WriteAsync(jsonResponse);
    }
}