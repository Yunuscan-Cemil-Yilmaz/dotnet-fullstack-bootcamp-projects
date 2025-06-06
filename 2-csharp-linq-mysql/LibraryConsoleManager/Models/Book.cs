using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryConsoleManager.Models;

public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Title { get; set; }
    public string? Genre { get; set; }
    public int PublishYear { get; set; }

    [ForeignKey("Author")]
    public int AuthorId { get; set; } // add to cascade delete while migrating
    public Author? Author { get; set; }
}