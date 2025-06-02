using System;
using System.Linq;
using ConsoleTodoApp.Config;
using ConsoleTodoApp.Models;

public static class Seeder
{
    public static void Run()
    {
        using (var context = new DBConnection())
        {
            int currentCount = context.Tasks.Count();
            if (currentCount >= 10)
            {
                Console.WriteLine("Database already seeded with 10 tasks. No action taken.");
                return;
            }

            int recordsToSeed = 10 - currentCount;
            for (int i = 0; i < recordsToSeed; i++)
            {
                var taskNo = Guid.NewGuid().ToString().Substring(0, 8);
                var task = new TaskItem
                {
                    Title = $"Sample-{taskNo}",
                    Description = $"This is a sample task description for task {taskNo}.",
                    IsCompleted = false,
                    CreatedAt = DateTime.Now,
                    CompletedAt = null,
                    UpdatedAt = DateTime.Now
                };

                context.Tasks.Add(task);
            }

            context.SaveChanges();
            Console.WriteLine($"Seeded {recordsToSeed} tasks into the database.");
            Console.WriteLine("Database seeding completed successfully.");
        }
    }
}
