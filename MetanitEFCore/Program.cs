using MetanitEFCore;
using Microsoft.EntityFrameworkCore;

//var user = GetFirstUser();
//UpdateUser(user);

//CreateTestUsersAsync();
//UpdateFirstAsync();
DeleteFirstAsync();

//CreateTestUsers();
//UpdateFirst();
//DeleteFirst();
PrintAsync();
Console.Read();

User? GetFirstUser()
{
    using var db = new ApplicationContext();
    return db.Users.FirstOrDefault();
}

void DeleteFirst()
{
    using var db = new ApplicationContext();
    var first = db.Users.FirstOrDefault();
    if (first == null) 
        return;
    db.Users.Remove(first);
    db.SaveChanges();
}
async void DeleteFirstAsync()
{
    await using var db = new ApplicationContext();
    var first = await db.Users.FirstOrDefaultAsync();
    if (first == null) 
        return;
    db.Users.Remove(first);
    await db.SaveChangesAsync();
}

void UpdateFirst()
{
    using var db = new ApplicationContext();
    var first = db.Users.FirstOrDefault();
    if (first == null) 
        return;
    first.Name = "Bobby";
    first.Age = 99;
    db.SaveChanges();
}
async void UpdateFirstAsync()
{
    await using var db = new ApplicationContext();
    var first = await db.Users.FirstOrDefaultAsync();
    if (first == null) 
        return;
    first.Name = "Bobby";
    first.Age = 99;
    await db.SaveChangesAsync();
}

void UpdateUser(User user)
{
    using var db = new ApplicationContext();
    if (user == null)
        return;
    user.Name = "Igorok";
    user.Age = 23;
    db.Users.Update(user);
    db.SaveChanges();
}

void CreateTestUsers()
{
    using var db = new ApplicationContext();
    
    var tom = new User { Name = "Tom", Age = 33 };
    var alice = new User { Name = "Alice", Age = 26 };
    
    db.Users.AddRange(tom, alice);
    db.SaveChanges();
    Console.WriteLine("Objects saved!");
}
async void CreateTestUsersAsync()
{
    await using var db = new ApplicationContext();
    
    var tom = new User { Name = "Tom", Age = 33 };
    var alice = new User { Name = "Alice", Age = 26 };
    
    await db.Users.AddRangeAsync(tom, alice);
    await db.SaveChangesAsync();
    Console.WriteLine("Objects saved!");
}

void Print()
{
    using var db = new ApplicationContext();
    var users = db.Users.ToList();
    
    Console.WriteLine("User list:");
    foreach(var user in users)
        Console.WriteLine($"{user.UserId}. {user.Name} - {user.Age}");
}
async void PrintAsync()
{
    await using var db = new ApplicationContext();
    var users = await db.Users.ToListAsync();
    
    Console.WriteLine("User list:");
    foreach(var user in users)
        Console.WriteLine($"{user.UserId}. {user.Name} - {user.Age}");
}