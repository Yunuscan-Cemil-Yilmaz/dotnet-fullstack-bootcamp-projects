using System;
using LibraryConsoleManager.Services;
using LibraryConsoleManager.Models;

namespace LibraryConsoleManager.Controllers;

public class BookController
{
    private readonly BookService _bookService;
    public BookController()
    {
        _bookService = new BookService();
    }

    public void Main()
    {
        int listSelection = 0;
        do
        {
            Console.WriteLine("Select a List Option");
            Console.WriteLine("1- List All Books (None Filter)");
            Console.WriteLine("2- List Books with Filter");
            Console.WriteLine("3- Find Book by Id");
            Console.Write("Enter your selection: ");
            listSelection = int.Parse(Console.ReadLine() ?? "0");
        } while (listSelection < 1 || listSelection > 3);

        switch (listSelection)
        {
            case 1:
                var books = _bookService.GetALlBooksList();
                if (books.Count == 0)
                {
                    Console.WriteLine("No books found.");
                    break;
                }
                foreach (var b in books)
                {
                    Console.WriteLine($"ID: {b.Id}\t Title: {b.Title}");
                }
                break;
            case 2:
                var filteredBooks = FilteredBookList();
                if (filteredBooks.Count == 0)
                {
                    Console.WriteLine("No books found with the specified filter.");
                    break;
                }
                foreach (var b in filteredBooks)
                {
                    Console.WriteLine($"ID: {b.Id}\t Title: {b.Title}");
                }
                break;
            case 3:
                Console.Write("Enter Book ID: ");
                int bookId = int.Parse(Console.ReadLine() ?? "0");
                var book = _bookService.GetBookById(bookId);
                if (book == null)
                {
                    Console.WriteLine($"Book with ID {bookId} not found.");
                    break;
                }
                Console.WriteLine($"ID: {book.Id}\t Title: {book.Title}");
                break;
        }

        Book selectedBook;
        bool isSelected = false;
        do
        {
            Console.Write("Select a Book with ID for Process (press -1 for exit): ");
            int id = int.Parse(Console.ReadLine() ?? "0");
            if (id == -1)
            {
                Console.WriteLine("Exiting book selection.");
                return;
            }
            selectedBook = _bookService.GetBookById(id);
            if (selectedBook != null) isSelected = true;
        } while (!isSelected);

        int actionChoice = 0;
        do
        {
            Console.WriteLine("Select an Action for the Book");
            Console.WriteLine("1- Update Book");
            Console.WriteLine("2- Delete Book");
            Console.WriteLine("3- Show Book Details");
            Console.WriteLine("4- Exit");
            Console.Write("Enter your selection: ");
            actionChoice = int.Parse(Console.ReadLine() ?? "0");
        } while (actionChoice < 1 || actionChoice > 4);

        switch (actionChoice)
        {
            case 1:
                int updateChoice = 0;
                do
                {
                    Console.WriteLine("Select an Update Option");
                    Console.WriteLine("1- Update Book Title");
                    Console.WriteLine("2- Update Book Genre");
                    Console.WriteLine("3- Update Book Publish Year");
                    Console.WriteLine("4- Exit Update");
                    Console.Write("Enter your selection: ");
                    updateChoice = int.Parse(Console.ReadLine() ?? "0");
                } while (updateChoice < 1 || updateChoice > 4);

                switch (updateChoice)
                {
                    case 1:
                        Console.Write("Enter New Title: ");
                        string newTitle = Console.ReadLine() ?? "";
                        _bookService.UpdateBookTitle(selectedBook.Id, newTitle);
                        break;
                    case 2:
                        Console.Write("Enter New Genre: ");
                        string newGenre = Console.ReadLine() ?? "";
                        _bookService.UpdateBookGenre(selectedBook.Id, newGenre);
                        break;
                    case 3:
                        Console.Write("Enter New Publish Year: ");
                        int newYear = int.Parse(Console.ReadLine() ?? "0");
                        _bookService.UpdateBookPublishYear(selectedBook.Id, newYear);
                        break;
                }
                break;
            case 2:
                _bookService.DeleteBook(selectedBook.Id);
                break;
            case 3:
                Console.WriteLine($"Book Title: {selectedBook.Title}");
                Console.WriteLine($"Book Genre: {selectedBook.Genre}");
                Console.WriteLine($"Book Publish Year: {selectedBook.PublishYear}");
                Console.WriteLine($"Author ID: {selectedBook.AuthorId}");
                Console.WriteLine($"Author Name: {selectedBook.Author?.Name}");
                Console.WriteLine($"Author Birth Year: {selectedBook.Author?.BirthYear}");
                Console.WriteLine($"Author Country: {selectedBook.Author?.Country}");
                break;
        }
    }

    private List<Book> FilteredBookList()
    {
        int filterChoice = 0;
        do
        {
            Console.WriteLine("Enter a Filter Choice");
            Console.WriteLine("1- Filter by Title");
            Console.WriteLine("2- Filter by Genre");
            Console.WriteLine("3- Filter by Publish Year");
            Console.WriteLine("4- None Filter");
            Console.Write("Enter your selection: ");
            filterChoice = int.Parse(Console.ReadLine() ?? "0");
        } while (filterChoice < 1 || filterChoice > 4);

        var books = _bookService.GetALlBooksList();

        switch (filterChoice)
        {
            case 1:
                Console.Write("Enter Book Title: ");
                string title = Console.ReadLine() ?? "";
                return books.Where(b => b.Title?.Contains(title, StringComparison.OrdinalIgnoreCase) == true).ToList();
            case 2:
                Console.Write("Enter Genre: ");
                string genre = Console.ReadLine() ?? "";
                return books.Where(b => b.Genre?.Contains(genre, StringComparison.OrdinalIgnoreCase) == true).ToList();
            case 3:
                Console.Write("Enter Publish Year: ");
                int year = int.Parse(Console.ReadLine() ?? "0");
                return books.Where(b => b.PublishYear == year).ToList();
            case 4:
            default:
                return books;
        }
    }
}
