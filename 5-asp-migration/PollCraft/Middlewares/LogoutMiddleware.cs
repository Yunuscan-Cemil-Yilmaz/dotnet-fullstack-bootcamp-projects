using System.Text.Json;
namespace PollCraft.Middlewares;

public class LogoutMiddleware 
{
    private readonly RequestDelegate _next;
    public LogoutMiddleware(RequestDelegate next)
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
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new {
                error = "somethings went wrong!"
            }));
        }
    }
}