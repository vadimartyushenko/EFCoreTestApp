using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MetanitEFCore;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    private readonly StreamWriter _logWriter = new("ef_logs.txt");

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        var isNotExist = Database.EnsureCreated();
        Console.WriteLine($"Database exist - {!isNotExist}");
        Console.WriteLine($"Db is available - {Database.CanConnect()}");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        EventId[]? events = null; //new[] { RelationalEventId.CommandExecuted };
        string[] categories = new[] { DbLoggerCategory.Database.Command.Name };
        if (events != null)
            optionsBuilder.LogTo(_logWriter.WriteLine, events);
        else if (categories != null)
            optionsBuilder.LogTo(_logWriter.WriteLine, categories);
        else
            optionsBuilder.LogTo(_logWriter.WriteLine, LogLevel.Information);
    }

    public override void Dispose()
    {
        base.Dispose();
        _logWriter.Dispose();
    }
}