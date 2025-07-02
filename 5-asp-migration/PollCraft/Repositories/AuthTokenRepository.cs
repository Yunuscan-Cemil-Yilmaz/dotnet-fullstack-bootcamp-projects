using Microsoft.EntityFrameworkCore;
using PollCraft.Infrastructure.Data;
using PollCraft.Models;

namespace PollCraft.Repositories;

public interface IAuthTokenRepository
{
    Task<string?> CreateTokenAsync(int userId);
    Task<bool> ValidateTokenAsync(int userId, string token);
    Task DeleteTokenByUserIdAsync(int userId);
}

public class AuthTokenRepository : IAuthTokenRepository
{
    private readonly AppDbContext _context;

    public AuthTokenRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string?> CreateTokenAsync(int userId)
    {
        var authToken = new AuthToken
        {
            UserId = userId,
            Token = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        };

        await _context.AuthTokens.AddAsync(authToken);
        await _context.SaveChangesAsync();
        return authToken.Token;
    }

    public async Task<bool> ValidateTokenAsync(int userId, string token)
    {
        var authToken = await _context.AuthTokens
            .FirstOrDefaultAsync(at => at.UserId == userId && at.Token == token && at.ExpiresAt > DateTime.UtcNow);

        return authToken != null;
    }

    public async Task DeleteTokenByUserIdAsync(int userId)
    {
        var tokens = _context.AuthTokens
            .Where(at => at.UserId == userId);
        _context.AuthTokens.RemoveRange(tokens);
        await _context.SaveChangesAsync();
    }
}