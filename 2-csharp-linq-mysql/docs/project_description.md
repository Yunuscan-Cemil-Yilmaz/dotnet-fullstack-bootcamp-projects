# LibraryConsoleManager

## Project Description

**LibraryConsoleManager** is a C# console-based application that simulates a simple library management system.  
It allows users to add, list, update, and delete books and authors through a terminal interface.  
All data is stored and managed using a **MySQL** database with **Entity Framework Core** integration.

This project was built as part of a personal .NET learning path and emphasizes real-world programming concepts such as **OOP**, **LINQ**, **EF Core**, and clean architecture design.

---

## Objectives

- Strengthen understanding of:
  - Console-based input/output
  - Object-oriented programming (Classes, Properties, Methods)
  - Clean architecture (Separation of Controllers, Services, and Models)
  - LINQ operations for filtering and querying
  - Entity Framework Core with MySQL
  - Working with nullable types and null safety
  - Writing modular, testable service logic
  - Managing foreign key relationships in code-first design

---

## Technologies Used

| Technology             | Purpose                              |
|------------------------|--------------------------------------|
| .NET 8 SDK             | Core application framework           |
| C#                     | Application logic                    |
| MySQL                  | Database                             |
| Entity Framework Core  | ORM for database communication       |
| LINQ                   | Querying in-memory collections       |
| MySql.EntityFrameworkCore | MySQL EF Core provider            |

---

## Features

- **Author Management**
  - Add, list, filter, update, delete authors
  - Show all books written by an author

- **Book Management**
  - Add, list, filter, update, delete books
  - Display the author's details for each book

- **Filtering Capabilities**
  - Filter authors by name, country, or birth year
  - Filter books by title, genre, or publish year

- **Safe Input Handling**
  - All user inputs are validated
  - Defensive coding against null and invalid references

- **Database Seeding**
  - Generate sample data for development using a command-line seed flag:
    ```bash
    dotnet run seed
    ```

---

## Learning Outcomes

- Hands-on experience with EF Core’s code-first approach
- Improved console-based CRUD interaction handling
- Practical knowledge of relationships (1-to-many) in relational databases
- Understanding of clean and layered architecture

---