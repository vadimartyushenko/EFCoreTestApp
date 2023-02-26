using Microsoft.EntityFrameworkCore;

namespace MetanitEFCore;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public ApplicationContext()
    {
        var isNotExist = Database.EnsureCreated();
        Console.WriteLine($"Database exist - {!isNotExist}");
        Console.WriteLine($"Db is available - {Database.CanConnect()}");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = users.db");
    }
}