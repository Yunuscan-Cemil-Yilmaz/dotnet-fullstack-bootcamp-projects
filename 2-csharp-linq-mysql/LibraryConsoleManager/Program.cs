using Bogus.Extensions.UnitedKingdom;
using LibraryConsoleManager.Config;
using LibraryConsoleManager.Models;
using LibraryConsoleManager.Seed;
using LibraryConsoleManager.Services;
using LibraryConsoleManager.Controllers;
using Microsoft.VisualBasic;

class Program
{
    private readonly AuthorController _authorController;
    private readonly BookController _bookController;
    public Program()
    {
        _authorController = new AuthorController();
        _bookController = new BookController();
    }

    static void Main(string[] args)
    {
        // seed args
        if (args.Length > 0 && args[0] == "seed") { SeedDatabase(); return; }

        var app = new Program();

        // run the application
        int selection = 0;
        do
        {
            selection = selectMenuOption();
            switch (selection)
            {
                case 1:
                app._bookController.Main();
                    break;
                case 2:
                    app._authorController.Main();
                    break;
                case 3:
                    AddNewBook();
                    break;
                case 4:
                    AddNewAuthor();
                    break;
                case 5:
                    Console.WriteLine("Thank you for using the Library Manager App!");
                    Console.WriteLine("Exiting the application...");
                    break;
            }
        } while (selection != 5);
    }

    private static void AddNewAuthor()
    {
        Console.WriteLine("Adding a new author...");
        var author = new Author();
        Console.Write("Enter author name: ");
        author.Name = Console.ReadLine();
        Console.Write("Enter author birth year: ");
        author.BirthYear = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Enter author country: ");
        author.Country = Console.ReadLine();
        var authorService = new AuthorService();
        authorService.AddAuthor(author);
    }

    private static void AddNewBook()
    {
        Console.WriteLine("Adding a new book...");
        var book = new Book();
        Console.Write("Enter book title: ");
        book.Title = Console.ReadLine();
        Console.Write("Enter book genre: ");
        book.Genre = Console.ReadLine();
        Console.Write("Enter book publish year: ");
        book.PublishYear = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Enter author ID: ");
        book.AuthorId = int.Parse(Console.ReadLine() ?? "0");
        var bookService = new BookService();
        bookService.AddBook(book);
    }

    private static int selectMenuOption()
    {
        int choise = 0;
        do
        {
            Console.WriteLine("==== Library Manager App ====");
            Console.WriteLine("1- List Books");
            Console.WriteLine("2- List Authors");
            Console.WriteLine("3- Add Book");
            Console.WriteLine("4- Add Author");
            Console.WriteLine("5- Exit");
            Console.Write("Select an option: ");
            choise = int.Parse(Console.ReadLine() ?? "0");
            if (choise < 1 || choise > 5)
            {
                Console.WriteLine("Invalid option, please try again.");
            }
        } while (choise < 1 || choise > 5);
        return choise;
    }

    private static void SeedDatabase()
    {
        Console.WriteLine("Start Seeding");
        using var context = new DBContext();
        DataSeeder.Seed(context);
        Console.WriteLine("Seeding Completed");
    }
}