using System;
using LibraryConsoleManager.Models;
using LibraryConsoleManager.Config;
using Microsoft.EntityFrameworkCore;

namespace LibraryConsoleManager.Services;

public class BookService
{
    public List<Book> GetALlBooksList()
    {
        using var context = new DBContext();
        return context.Books.ToList();
    }

    public Book? GetBookById(int id)
    {
        using var context = new DBContext();
        Book? book = context.Books
            .Include(b => b.Author) // Include related author if needed
            .FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            Console.WriteLine($"Book with ID {id} not found.");
            return null;
        }
        return book;
    }

    public int AddBook(Book book)
    {
        using var context = new DBContext();
        bool authorExists = context.Authors.Any(a => a.Id == book.AuthorId);
        if (!authorExists)
        {
            Console.WriteLine($"Author with ID {book.AuthorId} does not exist. Please add the author first.");
            return 404; // Bad Request
        }
        try
        {
            context.Books.Add(book);
            context.SaveChanges();
            Console.WriteLine($"Book '{book.Title}' added successfully.");
            return 200; // OK
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding book: {ex.Message}");
            return 500; // Internal Server Error
        }
    }

    public int UpdateBookTitle(int id, string newTitle)
    {
        using var context = new DBContext();
        try
        {
            var book = context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine($"Book with ID {id} not found.");
                return 404; // Not Found
            }
            book.Title = newTitle;
            context.SaveChanges();
            Console.WriteLine($"Book with ID {id} updated successfully.");
            return 200; // OK
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating book: {ex.Message}");
            return 500; // Internal Server Error
        }
    }

    public int UpdateBookGenre(int id, string newGenre)
    {
        using var context = new DBContext();
        try
        {
            var book = context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine($"Book with ID {id} not found.");
                return 404; // Not Found
            }
            book.Genre = newGenre;
            context.SaveChanges();
            Console.WriteLine($"Book with ID {id} updated successfully.");
            return 200; // OK
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating book: {ex.Message}");
            return 500; // Internal Server Error
        }
    }

    public int UpdateBookPublishYear(int id, int newYear)
    {
        using var context = new DBContext();
        try
        {
            var book = context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine($"Book with ID {id} not found.");
                return 404; // Not Found
            }
            book.PublishYear = newYear;
            context.SaveChanges();
            Console.WriteLine($"Book with ID {id} updated successfully.");
            return 200; // OK
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating book: {ex.Message}");
            return 500; // Internal Server Error
        }
    }

    public int DeleteBook(int id)
    {
        using var context = new DBContext();
        try
        {
            var book = context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine($"Book with ID {id} not found.");
                return 404; // Not Found
            }
            context.Books.Remove(book);
            context.SaveChanges();
            Console.WriteLine($"Book with ID {id} deleted successfully.");
            return 200; // OK
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting book: {ex.Message}");
            return 500; // Internal Server Error
        }
    }
}