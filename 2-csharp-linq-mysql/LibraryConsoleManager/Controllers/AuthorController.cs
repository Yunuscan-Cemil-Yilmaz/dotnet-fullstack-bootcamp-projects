using System;
using LibraryConsoleManager.Services;
using LibraryConsoleManager.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace LibraryConsoleManager.Controllers;

public class AuthorController
{
    private readonly AuthorService _authorService;
    public AuthorController()
    {
        _authorService = new AuthorService();
    }
    public void Main()
    {
        int listSelection = 0;
        do
        {
            Console.WriteLine("Select a List Option");
            Console.WriteLine("1- List All Authors (None Filter)");
            Console.WriteLine("2- List Authors with Filter");
            Console.WriteLine("3- Find Author by Id");
            Console.Write("Enter your selection: ");
            listSelection = int.Parse(Console.ReadLine() ?? "0");
        } while (listSelection != 1 && listSelection != 2 && listSelection != 3);
        switch (listSelection)
        {
            case 1:
                var authors = _authorService.GetAllAuthorsList();
                if (authors.Count == 0)
                {
                    Console.WriteLine("No authors found.");
                    break;
                }
                foreach (var a in authors)
                {
                    Console.WriteLine($"ID: {a.Id}\t Name: {a.Name}");
                }
                break;
            case 2:
                var authorsFiltered = FilteredAuthorList();
                if (authorsFiltered.Count == 0)
                {
                    Console.WriteLine("No authors found with the specified filter.");
                    break;
                }
                foreach (var a in authorsFiltered)
                {
                    Console.WriteLine($"ID: {a.Id}\t Name: {a.Name}");
                }
                break;
            case 3:
                Console.Write("Enter Author ID: ");
                int authorId = int.Parse(Console.ReadLine() ?? "0");
                Author? author = _authorService.GetAuthorById(authorId);
                if (author == null)
                {
                    Console.WriteLine($"Author with ID {authorId} not found.");
                    break;
                }
                Console.WriteLine($"ID: {author.Id}\t Name: {author.Name}");
                break;
            default:
                Console.WriteLine("Invalid selection. Please try again.");
                break;
        }

        bool isSelected = false;
        Author? selectedAuthor;
        do
        {
            Console.Write("Select an Author with ID for Process (press -1 for exit): ");
            int authorId = int.Parse(Console.ReadLine() ?? "0");
            if (authorId == -1)
            {
                Console.WriteLine("Exiting author selection.");
                isSelected = true;
                return;
            }
            selectedAuthor = _authorService.GetAuthorById(authorId);
            if (selectedAuthor == null) continue;
            else isSelected = true;
        } while (!isSelected);

        int actionChoice = 0;
        do
        {
            Console.WriteLine("Select an Action for the Author");
            Console.WriteLine("1- Update Author");
            Console.WriteLine("2- Delete Author");
            Console.WriteLine("3- Show Author Details");
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
                    Console.WriteLine("1- Update Author Name");
                    Console.WriteLine("2- Update Author Birth Year");
                    Console.WriteLine("3- Update Author Country");
                    Console.WriteLine("4- Exit Update");
                    Console.Write("Enter your selection: ");
                    updateChoice = int.Parse(Console.ReadLine() ?? "0");
                } while (updateChoice < 1 || updateChoice > 4);

                switch (updateChoice)
                {
                    case 1:
                        Console.Write("Enter New Author Name: ");
                        string newName = Console.ReadLine() ?? string.Empty;
                        _authorService.UpdateAuthorName(selectedAuthor.Id, newName);
                        break;
                    case 2:
                        Console.Write("Enter New Author Birth Year: ");
                        int newBirthYear = int.Parse(Console.ReadLine() ?? "0");
                        _authorService.UpdateAuthorBirthYear(selectedAuthor.Id, newBirthYear);
                        break;
                    case 3:
                        Console.Write("Enter New Author Country: ");
                        string newCountry = Console.ReadLine() ?? string.Empty;
                        _authorService.UpdateAuthorCountry(selectedAuthor.Id, newCountry);
                        break;
                    case 4:
                        Console.WriteLine("Exiting update options.");
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
                break;
            case 2:
                _authorService.DeleteAuthor(selectedAuthor.Id);
                break;
            case 3:
                Console.WriteLine($"Author Name: {selectedAuthor.Name}");
                Console.WriteLine($"Author Birth Year: {selectedAuthor.BirthYear}");
                Console.WriteLine($"Author Country: {selectedAuthor.Country}");
                Console.WriteLine("Books by this author:");
                if (selectedAuthor.Books.Count == 0)
                {
                    Console.WriteLine("No books found for this author.");
                }
                else
                {
                    foreach (var book in selectedAuthor.Books)
                    {
                        Console.WriteLine($"Book ID: {book.Id}\t Title: {book.Title}");
                    }
                }
                break;
            case 4:
                Console.WriteLine("Exiting author actions.");
                break;
            default:
                Console.WriteLine("Invalid selection. Please try again.");
                break;
        }

    }

    private List<Author> FilteredAuthorList()
    {
        int filterChoice = 0;
        do
        {
            Console.WriteLine("Enter a Filter Choise");
            Console.WriteLine("1- Filter by Name");
            Console.WriteLine("2- Filter by Birth Year");
            Console.WriteLine("3- Filter by Country");
            Console.WriteLine("4- None Filter");
            Console.Write("Enter your selection: ");
            filterChoice = int.Parse(Console.ReadLine() ?? "0");
        } while (filterChoice < 1 || filterChoice > 4);

        switch (filterChoice)
        {
            case 1:
                Console.Write("Enter Author Name: ");
                string name = Console.ReadLine() ?? string.Empty;
                return _authorService.GetAllAuthorsList().Where(a => a.Name?.Contains(name, StringComparison.OrdinalIgnoreCase) == true).ToList();
            case 2:
                Console.Write("Enter Birth Year: ");
                int birthYear = int.Parse(Console.ReadLine() ?? "0");
                return _authorService.GetAllAuthorsList().Where(a => a.BirthYear == birthYear).ToList();
            case 3:
                Console.Write("Enter Country: ");
                string country = Console.ReadLine() ?? string.Empty;
                return _authorService.GetAllAuthorsList().Where(a => a.Country?.Contains(country, StringComparison.OrdinalIgnoreCase) == true).ToList();
            case 4:
            default:
                return _authorService.GetAllAuthorsList();
        }
    }
}