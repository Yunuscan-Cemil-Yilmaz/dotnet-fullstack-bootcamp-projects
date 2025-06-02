using System;
using ConsoleTodoApp.Config;
using Microsoft.EntityFrameworkCore;

public static class MigrationRunner
{
    public static void Run()
    {
        Console.WriteLine("Running migration...");
        try
        {
            using (var context = new DBConnection())
            {
                context.Database.Migrate();
                Console.WriteLine("Migration completed successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during migration: {ex.Message}");
        }
    }
}