using AutoMapper;
using PollCraft.DTOs;
using PollCraft.Repositories;
using PollCraft.Models;

namespace PollCraft.Services;

public class UserService
{
    private readonly IUserRepository _repo;
    private readonly IMapper _mapper;
    private readonly IPasswordService _passwordService;

    public UserService(IUserRepository repo, IMapper mapper, IPasswordService passwordService)
    {
        _repo = repo;
        _mapper = mapper;
        _passwordService = passwordService;
    }

    public async Task RegisterAsync(RegisterRequestDto dto)
    {
        if (await _repo.EmailExistsAsync(dto.Email)) throw new Exception("Email already exists");

        var user = _mapper.Map<User>(dto);

        var hashedPassword = _passwordService.HashPassword(dto.Password);
        user.Password = hashedPassword;

        await _repo.AddUserAsync(user);
    }
}