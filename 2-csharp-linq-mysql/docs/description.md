# Project Technical Description

## Overview

LibraryConsoleManager is a modular, console-based C# application that provides CRUD functionalities for managing authors and books using a MySQL database. The project adheres to a layered architecture consisting of Controllers, Services, and Models.

---

## Technologies and Tools

### .NET Core (C#)
- The application is built using **.NET 8.0** and written in C#.
- It runs as a console application using the `Main()` entry point of the manager class.

### MySQL
- The data is stored in a **MySQL** database.
- Tables:
  - `Authors` (Id, Name, BirthYear, Country)
  - `Books` (Id, Title, Genre, PublishYear, AuthorId)
- The relationship between Authors and Books is one-to-many.

### Entity Framework Core (EF Core)
- Used as the ORM (Object-Relational Mapping) to interact with the MySQL database.
- DBContext class is defined in `Config/DBContext.cs`.
- Migrations are used to generate and apply schema changes.
- Sample DBContext configuration:
  ```csharp
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
      optionsBuilder.UseMySql(
          "server=localhost;user=root;password=yourpassword;database=LibraryDB",
          new MySqlServerVersion(new Version(8, 0, 28))
      );
  }
  ```

## Project Structure
* Controllers/

    * Handle console input/output and direct user flow.

    * Example: AuthorController.cs, BookController.cs.

* Services/

    * Contain the business logic and data access operations.

    * Example: AuthorService.cs, BookService.cs.

* Models/

    * Represent the database entities.

    * Example: Author.cs, Book.cs.

* Config/

    * Contains DBContext.cs for database configuration and migration context.

## LINQ, Exception Handling, and Entity Navigation

### LINQ Usage

Language Integrated Query (LINQ) allows writing SQL-like queries directly in C#. This project makes use of LINQ to filter and query data from in-memory collections.

#### Where it was used:

- Filtering authors by name, birth year, or country:
  ```csharp
  return _authorService.GetAllAuthorsList()
      .Where(a => a.Name?.Contains(name, StringComparison.OrdinalIgnoreCase) == true)
      .ToList();
  ```

LINQ simplifies readable and efficient data querying after loading data from the database.

### Exception Handling
Every database operation is wrapped in try-catch blocks.

Any failure during SaveChanges() or data access logs a meaningful error message to the console.

#### Example:
```csharp
try
{
    context.Authors.Add(author);
    context.SaveChanges();
}
catch (Exception ex)
{
    Console.WriteLine($"Error adding author: {ex.Message}");
}
```

### Entity Navigation and Include
The Book entity has a navigation property to Author:
```csharp
[ForeignKey("Author")]
public int AuthorId { get; set; }
public Author? Author { get; set; }
```
To retrieve a Book and its associated Author in a single query:
```csharp
var book = context.Books
    .Include(b => b.Author)
    .FirstOrDefault(b => b.Id == id);
```
This ensures the Author object is not null when accessed from the Book instance.

## Code Quality, Best Practices, and Lessons Learned

### Layered Architecture

- The application uses a **3-layer architecture**:
  - **Controllers** handle user interaction and input validation.
  - **Services** manage business logic and database access.
  - **Models** represent data structure mapped to the MySQL database.

This structure promotes maintainability and separation of concerns.

---

### Dependency Management

- Each controller initializes its respective service using class constructors.
- Services use `using var context = new DBContext();` to ensure proper disposal of database contexts, avoiding memory leaks or hanging connections.

---

### Nullable Warnings and Safety

- The application enables nullable reference checks to reduce potential null-dereference exceptions.
- Some warnings (e.g., CS8602) are acceptable in console applications where `null` checks are manually enforced.

---

### User Input Validation

- The application enforces correct user selections using `do-while` loops.
- All numeric inputs are parsed safely using `int.Parse(Console.ReadLine() ?? "0");`, and critical actions allow exiting with `-1`.

---

### Lessons Learned

- LINQ enhances readability and reduces manual iteration logic.
- Entity Framework Core's `.Include()` is essential for working with related entities (e.g., retrieving a book with its author).
- Handling nulls explicitly prevents runtime crashes.
- Designing a small-scale application with service and controller separation improves scalability and testability.
- Console apps can simulate real-world backend logic effectively, even without a front-end.

---

This concludes the technical breakdown of the **LibraryConsoleManager** project.