using Microsoft.AspNetCore.Mvc;
using PollCraft.Infrastructure.Data;
using PollCraft.Repositories;

public class ApplicationViewMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _scopeFactory;

    public ApplicationViewMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
    {
        _next = next;
        _scopeFactory = scopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var repo = scope.ServiceProvider.GetRequiredService<IAuthTokenRepository>();

        try
        {
            var segments = context.Request.Path.Value?
                .Split('/', StringSplitOptions.RemoveEmptyEntries);

            var token = segments?.Length >= 2 ? segments[^2] : null;

            int userId = 0;
            if (segments?.Length >= 1 && int.TryParse(segments[^1], out var parsedUserId))
            {
                userId = parsedUserId;
            }

            bool tokenCheck = await repo.ValidateTokenAsync(userId, token);
            if (!tokenCheck)
            {
                context.Response.Redirect("/Errors/Unauthorized");
                return;
            }

            await _next(context);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("hata : " + ex);
            context.Response.Redirect("/Errors/SystemError");
        }
    }
}
