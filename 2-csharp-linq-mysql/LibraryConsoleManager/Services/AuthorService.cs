using System;
using LibraryConsoleManager.Config;
using LibraryConsoleManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryConsoleManager.Services;

public class AuthorService
{
    public List<Author> GetAllAuthorsList()
    {
        using var context = new DBContext();
        return context.Authors.ToList();
    }

    public Author? GetAuthorById(int id)
    {
        using var context = new DBContext();
        Author? author = context.Authors
            .Include(a => a.Books) // Include related books if needed
            .FirstOrDefault(a => a.Id == id);
        if (author == null)
        {
            Console.WriteLine($"Author with ID {id} not found.");
            return null;
        }
        return author;
    }

    public int AddAuthor(Author author)
    {
        using var context = new DBContext();
        try
        {
            context.Authors.Add(author);
            context.SaveChanges();
            Console.WriteLine($"Author {author.Name} added successfully.");
            return 200;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding author: {ex.Message}");
            return 500;
        }
    }

    public int UpdateAuthorName(int id, string newName)
    {
        using var context = new DBContext();
        try
        {
            var author = context.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                Console.WriteLine($"Author with ID {id} not found.");
                return 404;
            }
            author.Name = newName;
            context.SaveChanges();
            Console.WriteLine($"Author with ID {id} updated successfully.");
            return 200;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating author: {ex.Message}");
            return 500;
        }
    }

    public int UpdateAuthorCountry(int id, string newCountry)
    {
        using var context = new DBContext();
        try
        {
            var author = context.Authors.FirstOrDefault(async => async.Id == id);
            if (author == null)
            {
                Console.WriteLine($"Author with ID {id} not found.");
                return 404;
            }
            author.Country = newCountry;
            context.SaveChanges();
            Console.WriteLine($"Author with ID {id} updated successfully.");
            return 200;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating author: {ex.Message}");
            return 500;
        }
    }

    public int UpdateAuthorBirthYear(int id, int newBirthYear)
    {
        using var context = new DBContext();
        try
        {
            var author = context.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                Console.WriteLine($"Author with ID {id} not found.");
                return 404;
            }
            author.BirthYear = newBirthYear;
            context.SaveChanges();
            Console.WriteLine($"Author with ID {id} updated successfully.");
            return 200;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating author: {ex.Message}");
            return 500;
        }
    }

    public int UpdateAuthor(Author author)
    {
        using var context = new DBContext();
        try
        {
            context.Authors.Update(author);
            context.SaveChanges();
            Console.WriteLine($"Author {author.Name} updated successfully.");
            return 200;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating author: {ex.Message}");
            return 500;
        }
    }

    public int DeleteAuthor(int id)
    {
        using var context = new DBContext();
        try
        {
            var author = context.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                Console.WriteLine($"Author with ID {id} not found.");
                return 404;
            }
            context.Authors.Remove(author);
            context.SaveChanges();
            Console.WriteLine($"Author with ID {id} deleted successfully.");
            return 200;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting author: {ex.Message}");
            return 500;
        }
    }
}