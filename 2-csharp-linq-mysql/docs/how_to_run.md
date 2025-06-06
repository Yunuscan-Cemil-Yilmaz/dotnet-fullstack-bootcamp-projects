# How to Run LibraryConsoleManager

This guide provides **step-by-step instructions** to install dependencies, configure the database, run migrations, seed data, and start the LibraryConsoleManager application.

---

## Step 1: Install Required Dependencies

> Make sure you are inside the project root directory before running the commands.

Install the following NuGet packages:

```bash
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.0
dotnet add package Bogus
```
If you're unsure whether packages are installed correctly, you can check them with:
```bash
dotnet list package
```

## Step 2: Configure Database Connection
Open the file: Config/DBContext.cs
Locate the connection string:
```bash
var connectionString = "server=localhost;database=library_menager_cs;user=yuncemaz;password=53231323";
```
and change this for yourself

## Step 3: Apply Migrations and Seeders
```bash
dotnet ef database update
```
```bash
dotnet run seed
```

## Step 4: Run the App
```bash
dotnet run
```