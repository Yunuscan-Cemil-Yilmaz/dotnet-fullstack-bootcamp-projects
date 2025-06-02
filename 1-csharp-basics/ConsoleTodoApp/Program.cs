using System;

class Program
{
    static void Main()
    {
        string? choice = null;
        while (choice != "1" && choice != "2" && choice != "3" && choice != "4"){
            ShowMenu();
            Console.Write("Your choice: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    TodoApp.Run();
                    break;
                case "2":
                    MigrationRunner.Run();
                    break;
                case "3":
                    Seeder.Run();
                    break;
                case "4":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void ShowMenu(){
        Console.WriteLine("=== Console Todo App ===");
        Console.WriteLine("1 - Run App");
        Console.WriteLine("2 - Run Migration");
        Console.WriteLine("3 - Run Seeder");
        Console.WriteLine("4 - Exit");
        Console.WriteLine("========================");
    }
}