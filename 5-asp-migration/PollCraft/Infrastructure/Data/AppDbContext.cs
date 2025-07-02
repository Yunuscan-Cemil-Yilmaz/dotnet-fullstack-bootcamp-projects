using Microsoft.EntityFrameworkCore;
using PollCraft.Models;

namespace PollCraft.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } // add table for User model
    public DbSet<AuthToken> AuthTokens { get; set; }

    public override int SaveChanges() // implement SaveChanges to update timestamps
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) // implement SaveChangesAsync to update timestamps
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }
    private void UpdateTimestamps() // function to update timestamps
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is ITimestampEntity &&
                       (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (ITimestampEntity)entry.Entity;
            var now = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = now;
            }

            if (entry.State == EntityState.Modified)
            {
                entity.UpdatedAt = now;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthToken>()
            .HasOne(a => a.User)
            .WithOne()
            .HasForeignKey<AuthToken>(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade); // set up one-to-one relationship with User and AuthToken
    }

}