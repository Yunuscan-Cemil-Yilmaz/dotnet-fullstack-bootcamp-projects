using System.ComponentModel.DataAnnotations;

namespace PollCraft.DTOs;

public class UserResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string ProfilePicture { get; set; }
}

public class LoginResponseDto
{
    public required UserResponse UserResponse { get; set; }
    public required string Token { get; set; }
}