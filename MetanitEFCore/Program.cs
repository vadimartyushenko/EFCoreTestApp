using MetanitEFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory());
builder.AddJsonFile("appsettings.json");

var config = builder.Build();
var connectionString = config.GetConnectionString("DefaultConnection");

var optionBuilder = new DbContextOptionsBuilder<ApplicationContext>();
var options = optionBuilder.UseSqlite(connectionString).Options;

using var db = new ApplicationContext(options);
var user = GetFirstUser(db);
//UpdateUser(user, db);

//CreateTestUsersAsync(options);
//UpdateFirstAsync(options);
//DeleteFirstAsync(options);

//CreateTestUsers(options);
//UpdateFirst(db);
//DeleteFirst(options);
PrintAsync(db);
Console.Read();

User? GetFirstUser(ApplicationContext db) => db.Users.FirstOrDefault();

void DeleteFirst(ApplicationContext db)
{
    var first = db.Users.FirstOrDefault();
    if (first == null) 
        return;
    db.Users.Remove(first);
    db.SaveChanges();
}
async void DeleteFirstAsync(ApplicationContext db)
{
    var first = await db.Users.FirstOrDefaultAsync();
    if (first == null) 
        return;
    db.Users.Remove(first);
    await db.SaveChangesAsync();
}

void UpdateFirst(ApplicationContext db)
{
    var first = db.Users.FirstOrDefault();
    if (first == null) 
        return;
    first.Name = "Bobby";
    first.Age = 99;
    db.SaveChanges();
}

async void UpdateFirstAsync(ApplicationContext db)
{
    var first = await db.Users.FirstOrDefaultAsync();
    if (first == null) 
        return;
    first.Name = "Bobby";
    first.Age = 99;
    await db.SaveChangesAsync();
}

void UpdateUser(User user, ApplicationContext db)
{
    if (user == null)
        return;
    user.Name = "Igorok";
    user.Age = 23;
    db.Users.Update(user);
    db.SaveChanges();
}

void CreateTestUsers(ApplicationContext db)
{
    var tom = new User { Name = "Tom", Age = 33 };
    var alice = new User { Name = "Alice", Age = 26 };
    
    db.Users.AddRange(tom, alice);
    db.SaveChanges();
    Console.WriteLine("Objects saved!");
}

async void CreateTestUsersAsync(ApplicationContext db)
{
    var tom = new User { Name = "Tom", Age = 33 };
    var alice = new User { Name = "Alice", Age = 26 };
    
    await db.Users.AddRangeAsync(tom, alice);
    await db.SaveChangesAsync();
    Console.WriteLine("Objects saved!");
}

void Print(ApplicationContext db)
{
    var users = db.Users.ToList();
    
    Console.WriteLine("User list:");
    foreach(var user in users)
        Console.WriteLine($"{user.UserId}. {user.Name} - {user.Age}");
}

async void PrintAsync(ApplicationContext db)
{
    var users = await db.Users.ToListAsync();
    
    Console.WriteLine("User list:");
    foreach(var user in users)
        Console.WriteLine($"{user.UserId}. {user.Name} - {user.Age}");
}