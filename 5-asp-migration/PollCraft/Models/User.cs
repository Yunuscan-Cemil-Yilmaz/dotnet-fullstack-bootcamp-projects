using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore; // for index attribute

namespace PollCraft.Models;

[Table("Users")]
[Index(nameof(Email), IsUnique = true)]
public class User : ITimestampEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    [MinLength(2)]
    public string? Name { get; set; }
    [Required]
    [MaxLength(50)]
    [MinLength(2)]
    public string? LastName { get; set; }
    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username can only contain letters, numbers and _")]
    public string? UserName { get; set; }
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string? Email { get; set; }
    [Required]
    [StringLength(255)]
    public string? Password { get; set; }
    [StringLength(500)]
    [Url]
    public string? ProfilePicture { get; set; } = "http://localhost:5182/uploads/pp/default.webp"; // Default value

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}