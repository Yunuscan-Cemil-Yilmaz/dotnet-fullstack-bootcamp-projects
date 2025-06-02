using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleTodoApp.Models;

public class TaskItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]

    // [Column("created_at")] -> if u want to change the column name in the database
    // [Column(TypeName = "varchar(255)")] -> if you want to change the type of the column in the database
    public string Title { get; set; } = string.Empty;


    public string Description { get; set; } = string.Empty;

    public bool IsCompleted { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? CompletedAt { get; set; } = null;

    public DateTime? UpdatedAt { get; set; } = null;

    // empty parameterless constructor for EF Core
    public TaskItem() { }

    // constructor with parameters for easy instantiation
    public TaskItem(string title, string description, bool? isCompleted = null, DateTime? createdAt = null, DateTime? updatedAt = null, DateTime? completedAt = null)
    {
        this.Title = title;
        this.Description = description;
        this.IsCompleted = isCompleted ?? false;
        this.CreatedAt = createdAt ?? DateTime.Now;
        this.UpdatedAt = updatedAt ?? DateTime.Now;
        this.CompletedAt = completedAt; 
    }
} 
