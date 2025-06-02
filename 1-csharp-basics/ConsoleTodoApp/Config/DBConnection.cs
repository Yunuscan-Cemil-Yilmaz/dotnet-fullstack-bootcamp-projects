using Microsoft.EntityFrameworkCore;
using ConsoleTodoApp.Models;
using ConsoleTodoApp.Config;

namespace ConsoleTodoApp.Config;

public class DBConnection : DbContext
{
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(DBConfig.ConnectionString, ServerVersion.AutoDetect(DBConfig.ConnectionString));
    }
}
