
using System.Text.Json;
namespace PollCraft.Middlewares;

public class RegisterMiddleware
{
    private readonly RequestDelegate _next;

    public RegisterMiddleware(RequestDelegate next)
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
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                error = ex.Message,
            }));
        }
    }
}