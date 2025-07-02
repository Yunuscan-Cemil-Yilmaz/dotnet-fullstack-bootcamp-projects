using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore; 

namespace PollCraft.Models;

[Table("AuthTokens")]
public class AuthToken
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    [Required]
    [StringLength(255)]
    public string Token { get; set; } = string.Empty;

    [Required]
    public DateTime? CreatedAt { get; set; }

    [Required]
    public DateTime? ExpiresAt { get; set; }
}