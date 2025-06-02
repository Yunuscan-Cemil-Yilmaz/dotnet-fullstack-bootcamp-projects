# ConsoleTodoApp - Project Description

This project is a simple console-based Todo application built using C# and Entity Framework Core with MySQL integration. It is designed to demonstrate basic CRUD operations in a layered and maintainable structure using .NET Core in a command-line environment.

## Features

- **Menu-driven console interface** for user interaction
- **Add, list, edit, and delete** todo tasks
- **Persistence using MySQL database** via Entity Framework Core
- **Auto-migration support** using `context.Database.Migrate()`
- **Database seeding** with optional logic to prevent redundant entries
- **Timestamps**: `CreatedAt`, `UpdatedAt`, and `CompletedAt` fields for better task tracking

## Technologies Used

- **.NET SDK 8.0**
- **C# 12**
- **Entity Framework Core (EF Core)**
- **MySQL**
- **LINQ** for data querying
- **EF Migrations** for schema changes
- **Console.ReadLine/WriteLine** for interaction

## Project Structure

ConsoleTodoApp/
│
├── Models/
│ └── TaskItem.cs # Entity model representing a Todo task
│
├── Config/
│ └── DBConfig.cs # Static class storing the database connection string
│ └── DBConnection.cs # DbContext configuration for MySQL
│
├── Migrations/
│ └── *.cs # EF migration scripts and snapshot
│
├── TodoApp.cs # Core application logic and menu interaction
├── Seeder.cs # Seeding logic to populate the DB
├── MigrationRunner.cs # Applies database migrations
├── Program.cs # Entry point with main menu loop
└── ConsoleTodoApp.csproj # Project file


## Design Considerations

- Encapsulation is achieved using static utility classes (`TodoApp`, `Seeder`, etc.)
- The application is designed to be **extensible**, with clear separation of database logic and UI logic
- The data model uses **PascalCase** in C# but can be modified to map to `snake_case` if needed

This project is ideal for those learning .NET Core with MySQL and understanding how to build structured CLI tools.