using Microsoft.EntityFrameworkCore;
using PollCraft.Infrastructure.Data;
using PollCraft.Services;
using PollCraft.Models;

namespace PollCraft.Repositories;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task<User?> LoginUserAsync(string email, string password);
    Task<bool> EmailExistsAsync(string email);
}

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly IPasswordService _passwordService;

    public UserRepository(AppDbContext context, IPasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> LoginUserAsync(string email, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null) return null;
        bool checkPsw = _passwordService.VerifyPassword(password, user.Password!);
        if (!checkPsw) return null;
        else if (checkPsw) return user;
        else return null;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
}