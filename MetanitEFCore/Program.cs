using System.Diagnostics;
using MetanitEFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");

        var config = builder.Build();
        var connectionString = config.GetConnectionString("DefaultConnection");

        var optionBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionBuilder.LogTo(message => Debug.WriteLine(message));
        var options = optionBuilder.UseSqlite(connectionString).Options;

        using var db = new ApplicationContext(options);
        var user = GetFirstUser();
//UpdateUser(user, db);

//CreateTestUsersAsync(options);
//UpdateFirstAsync(options);
        //DeleteFirstAsync();

        //CreateTestUsers(options);
        //UpdateFirst();
        //DeleteFirst(options);
        PrintAsync();
        Console.Read();

        User? GetFirstUser() => db?.Users.FirstOrDefault();

        void DeleteFirst()
        {
            var first = db?.Users.FirstOrDefault();
            if (first == null) 
                return;
            db?.Users.Remove(first);
            db?.SaveChanges();
        }
        async void DeleteFirstAsync()
        {
            if (db == null)
                throw new ArgumentNullException("db", "Database context not initialized");
            
            var first = await db.Users.FirstOrDefaultAsync();
            if (first == null) 
                return;
            db.Users.Remove(first);
            await db.SaveChangesAsync();
        }

        void UpdateFirst()
        {
            var first = db?.Users.FirstOrDefault();
            if (first == null) 
                return;
            first.Name = "Alan";
            first.Age = 18;
            db?.SaveChanges();
        }

        async void UpdateFirstAsync()
        {
            if (db == null)
                throw new ArgumentNullException("db", "Database context not initialized");
            var first = await db.Users.FirstOrDefaultAsync();
            if (first == null) 
                return;
            first.Name = "Bobby";
            first.Age = 99;
            await db.SaveChangesAsync();
        }

        void UpdateUser(User user)
        {
            if (user == null)
                return;
            user.Name = "Igorok";
            user.Age = 23;
            db?.Users.Update(user);
            db?.SaveChanges();
        }

        void CreateTestUsers()
        {
            var tom = new User { Name = "Tom", Age = 33 };
            var alice = new User { Name = "Alice", Age = 26 };
    
            db?.Users.AddRange(tom, alice);
            db?.SaveChanges();
            Console.WriteLine("Objects saved!");
        }

        async void CreateTestUsersAsync()
        {
            if (db == null)
                throw new ArgumentNullException("db", "Database context not initialized");
            var tom = new User { Name = "Tom", Age = 33 };
            var alice = new User { Name = "Alice", Age = 26 };
            await db.Users.AddRangeAsync(tom, alice);
            await db.SaveChangesAsync();
            Console.WriteLine("Objects saved!");
        }

        void Print()
        {
            var users = db?.Users.ToList();
            if (users == null)
                return;
            Console.WriteLine("User list:");
            foreach(var user in users)
                Console.WriteLine($"{user.UserId}. {user.Name} - {user.Age}");
        }

        async void PrintAsync()
        {
            var users = await db.Users.ToListAsync();
            Console.WriteLine("User list:");
            foreach(var user in users)
                Console.WriteLine($"{user.UserId}. {user.Name} - {user.Age}");
        }
    }
}