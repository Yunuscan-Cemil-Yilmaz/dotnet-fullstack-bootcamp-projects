using System;
using System.Linq;
using System.Net.NetworkInformation;
using ConsoleTodoApp.Config;
using ConsoleTodoApp.Models;

public static class TodoApp
{
    public static void Run()
    {
        string input;
        do
        {
            Console.WriteLine("=== Console Todo App ===");
            Console.WriteLine("1 - List Todos");
            Console.WriteLine("2 - Add Todo");
            Console.WriteLine("3 - Exit");
            Console.WriteLine("========================");
            Console.Write("Your choice: ");
            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ListTodos();
                    break;
                case "2":
                    AddNewTask();
                    break;
                case "3":
                    Console.WriteLine("Exiting Todo App...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        } while (input != "3");
    }

private static void ListTodos()
{
    using var context = new DBConnection();
    var tasks = context.Tasks.ToList();

    if (tasks.Count == 0)
    {
        Console.WriteLine("No tasks found.");
        return; 
    }

    Console.WriteLine("=== Todo List ===");
    foreach (var task in tasks)
    {
        Console.WriteLine($"ID: {task.Id}, Title: {task.Title}, Description: {task.Description}, Completed: {task.IsCompleted}, Created At: {task.CreatedAt}");
    }
    Console.WriteLine("===================");

    TaskItem? selectedTask = null;
    int selectedId;
    bool isValidSelection = false; 

    do
    {
        Console.Write("Select a task Id for make process (-1 for exit): ");
        string idInput = Console.ReadLine();
        if (int.TryParse(idInput, out selectedId))
        {
            if (selectedId == -1) 
            {
                Console.WriteLine("Exiting task selection.");
                return; 
            }

            selectedTask = tasks.FirstOrDefault(t => t.Id == selectedId);
            if (selectedTask == null)
            {
                Console.WriteLine("Task not found. Please try again.");
                isValidSelection = false;
            }
            else
            {
                isValidSelection = true;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number.");
            isValidSelection = false;
        }
    } while (!isValidSelection); 

    string action = "";
    do
    {
        Console.WriteLine("\n=== Todo Actions ==="); 
        Console.WriteLine("1 - Show Todo Details");
        Console.WriteLine("2 - Edit Todo"); 
        Console.WriteLine("3 - Delete Todo");
        Console.WriteLine("4 - Exit Actions"); 
        Console.Write("Your choice: ");
        action = Console.ReadLine();

        switch (action)
        {
            case "1":
                Console.WriteLine($"ID: {selectedTask.Id}\nTitle: {selectedTask.Title}\nDescription: {selectedTask.Description}\nCompleted: {selectedTask.IsCompleted}\nCreated At: {selectedTask.CreatedAt}\nUpdated At: {selectedTask.UpdatedAt}\nCompleted At: {selectedTask.CompletedAt}");
                break;
            case "2":
                if (selectedTask != null)
                {
                    string editChoice;
                        do
                        {
                            Console.WriteLine("\n--- Edit Todo Options ---"); 
                            Console.WriteLine("1 - Edit Title");
                            Console.WriteLine("2 - Edit Description");
                            Console.WriteLine("3 - Toggle Completion Status (IsCompleted)");
                            Console.WriteLine("4 - Back to Actions Menu"); 
                            Console.Write("Your choice: ");
                            editChoice = Console.ReadLine();

                            switch (editChoice)
                            {
                                case "1":
                                    Console.Write($"Enter new title (current: {selectedTask.Title}): ");
                                    selectedTask.Title = Console.ReadLine();
                                    selectedTask.UpdatedAt = DateTime.Now; 
                                    context.SaveChanges();
                                    Console.WriteLine("Title updated successfully.");
                                    break;
                                case "2":
                                    Console.Write($"Enter new description (current: {selectedTask.Description}): ");
                                    selectedTask.Description = Console.ReadLine();
                                    selectedTask.UpdatedAt = DateTime.Now;
                                    context.SaveChanges();
                                    Console.WriteLine("Description updated successfully.");
                                    break;
                                case "3":
                                    Console.WriteLine($"Current completion status: {(selectedTask.IsCompleted ? "Completed" : "Not Completed")}");
                                    Console.Write("Mark as completed? (yes/no): ");
                                    string? completionInput = Console.ReadLine()?.ToLower();
                                    if (completionInput == "yes")
                                    {
                                        if (!selectedTask.IsCompleted) 
                                        {
                                            selectedTask.IsCompleted = true;
                                            selectedTask.CompletedAt = DateTime.Now; 
                                            selectedTask.UpdatedAt = DateTime.Now;
                                            context.SaveChanges();
                                            Console.WriteLine("Task marked as completed.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Task is already marked as completed.");
                                        }
                                    }
                                    else if (completionInput == "no")
                                    {
                                        if (selectedTask.IsCompleted) 
                                        {
                                            selectedTask.IsCompleted = false;
                                            selectedTask.CompletedAt = null; 
                                            selectedTask.UpdatedAt = DateTime.Now;
                                            context.SaveChanges();
                                            Console.WriteLine("Task marked as not completed.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Task is already marked as not completed.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                                    }
                                    break;
                                case "4":
                                    Console.WriteLine("Returning to Actions Menu...");
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice. Please try again.");
                                    break;
                            }
                        } while (editChoice != "4");
                }
                else
                {
                    Console.WriteLine("No task selected for editing.");
                }
                break;
            case "3": 
                context.Tasks.Remove(selectedTask);
                context.SaveChanges();
                Console.WriteLine("Todo deleted successfully.");
                action = "4"; 
                break;
            case "4": 
                Console.WriteLine("Exiting Todo Actions...");
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    } while (action != "4");
}

    private static void AddNewTask()
    {
        using var context = new DBConnection();
        Console.Write("Enter task title: ");
        string? title = Console.ReadLine();
        Console.Write("Enter task description: ");
        string? description = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
        {
            Console.WriteLine("Title or description cannot be empty.");
            return;
        }
        var todo = new TaskItem
        {
            Title = title,
            Description = description,
            IsCompleted = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        context.Tasks.Add(todo);
        context.SaveChanges();
        Console.WriteLine("Task added successfully.");
    }
}
