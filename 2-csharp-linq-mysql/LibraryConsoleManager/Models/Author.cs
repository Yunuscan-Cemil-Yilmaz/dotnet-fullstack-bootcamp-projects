using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryConsoleManager.Models;

public class Author
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Name { get; set; }
    public int? BirthYear { get; set; }
    public string? Country { get; set; }

    public List<Book> Books { get; set; } = new();
}
