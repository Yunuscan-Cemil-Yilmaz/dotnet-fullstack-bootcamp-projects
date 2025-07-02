using System.Text.Json;

namespace PollCraft.Middlewares;

public class LoginMiddleware
{
    private readonly RequestDelegate _next;

    public LoginMiddleware(RequestDelegate next)
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
            context.Response.ContentType = "application/json";

            var statusCode = ex.Message switch
            {
                "User Not Found" => StatusCodes.Status404NotFound,
                "Unexpected Error" => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = statusCode;

            var errorResponse = new
            {
                response = (object?)null,
                token = (string?)null
            };

            var json = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(json);
        }
    }
}
