using Microsoft.EntityFrameworkCore;

namespace MetanitEFCore;

public class AppContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public AppContext()
    {
        var isNotExist = Database.EnsureCreated();
        Console.WriteLine($"Database exist - {!isNotExist}");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = users.db");
    }
}