using Microsoft.AspNetCore.Mvc;
using PollCraft.Services;
using PollCraft.DTOs;

namespace PollCraft.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;

    public AuthController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
    {
        await _userService.RegisterAsync(dto);
        return Ok(new { message = "User registered successfully" });
    }
    [HttpPost("login")]
    public async Task<IActionResult> login([FromBody] LoginRequestDto dto)
    {
        var loginResponse = await _userService.LoginAsync(dto);
        return Ok(new { message = "succecss", response = loginResponse });
    } 
}