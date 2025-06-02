# How to Run ConsoleTodoApp

This guide explains how to set up and run the ConsoleTodoApp project on your local machine.

## Prerequisites

Make sure you have the following installed:

- [.NET SDK 8.0 or later](https://dotnet.microsoft.com/en-us/download)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- A terminal/command-line environment (Linux, macOS, or Windows)


## Step-by-Step Setup

cd ConsoleTodoApp

### Configure MySQL Database
If your MySQL username or password is different, update it in:
Config/DBConfig.cs:
public const string ConnectionString = "server=localhost;port=3306;database=todo_app_cs;user=your_mysql_user;password=your_password;";

### Restore and build the project

```bash
dotnet restore
dotnet build
```

### Run the Application
#### Execute Migrations
```bash
dotnet run
```

you will see a menu like this:
```bash
=== Console Todo App ===
1 - Run App
2 - Run Migration
3 - Run Seeder
4 - Exit
========================
Your choice:
```

select 2 to execute database migrations

#### Seed the Database
```bash
dotnet run
```

you will see a menu like this:
```bash
=== Console Todo App ===
1 - Run App
2 - Run Migration
3 - Run Seeder
4 - Exit
========================
Your choice:
```

select 3:
This will insert sample task entries into the Tasks table (only if there are less than 10).


## Start using the application
```bash
dotnet run
```

you will see a menu like this:
```bash
=== Console Todo App ===
1 - Run App
2 - Run Migration
3 - Run Seeder
4 - Exit
========================
Your choice:
```

select 1 and start!


## Note: Library Dependencies

This project uses several external libraries such as:

- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Design`
- `Pomelo.EntityFrameworkCore.MySql`

Make sure all required NuGet packages are installed. If you encounter missing library errors, check the [`libs.md`](./libs.md) file and install them using:
```bash
dotnet add package MySql.Data
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.0
```