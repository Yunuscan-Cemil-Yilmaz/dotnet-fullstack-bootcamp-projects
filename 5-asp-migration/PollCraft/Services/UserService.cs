using AutoMapper;
using PollCraft.DTOs;
using PollCraft.Repositories;
using PollCraft.Models;

namespace PollCraft.Services;

public class UserService
{
    private readonly IUserRepository _repo;
    private readonly IAuthTokenRepository _tokenRepo;
    private readonly IMapper _mapper;
    private readonly IPasswordService _passwordService;

    public UserService(
        IUserRepository repo,
        IMapper mapper,
        IPasswordService passwordService,
        IAuthTokenRepository authTokenRepository
    ) {
        _repo = repo;
        _mapper = mapper;
        _passwordService = passwordService;
        _tokenRepo = authTokenRepository;
    }

    public async Task RegisterAsync(RegisterRequestDto dto)
    {
        if (await _repo.EmailExistsAsync(dto.Email)) throw new Exception("Email already exists");
        
        var user = _mapper.Map<User>(dto);

        var hashedPassword = _passwordService.HashPassword(dto.Password);
        user.Password = hashedPassword;

        await _repo.AddUserAsync(user);
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
    {
        var user = await _repo.LoginUserAsync(dto.Email, dto.Password);
        if (user == null) throw new Exception("User Not Found");
        await _tokenRepo.DeleteTokenByUserIdAsync(user.Id);
        string token = await _tokenRepo.CreateTokenAsync(user.Id);
        if (token == null) throw new Exception("Unexpected Error");

        var userResponse = new UserResponse
        {
            Id = user.Id,
            Name = user.Name!,
            LastName = user.LastName!,
            UserName = user.UserName!,
            Email = user.Email!,
            ProfilePicture = user.ProfilePicture
        };

        var loginResponse = new LoginResponseDto
        {
            UserResponse = userResponse,
            Token = token,
        };

        return loginResponse;
    }

    public async Task LogoutAsync(int userId){
        await _tokenRepo.DeleteTokenByUserIdAsync(userId);
    }
}