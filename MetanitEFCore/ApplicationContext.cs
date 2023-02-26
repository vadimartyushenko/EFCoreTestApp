using Microsoft.EntityFrameworkCore;

namespace MetanitEFCore;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        var isNotExist = Database.EnsureCreated();
        Console.WriteLine($"Database exist - {!isNotExist}");
        Console.WriteLine($"Db is available - {Database.CanConnect()}");
    }
}