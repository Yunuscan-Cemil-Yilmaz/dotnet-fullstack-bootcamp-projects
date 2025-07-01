using Microsoft.EntityFrameworkCore;
using PollCraft.Infrastructure.Data;
using PollCraft.Models;

namespace PollCraft.Repositories;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task<bool> EmailExistsAsync(string email);
}

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
}