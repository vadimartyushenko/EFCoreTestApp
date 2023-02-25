using MetanitEFCore;

using var db = new ApplicationContext();

var tom = new User { Name = "Tom", Age = 33 };
var alice = new User { Name = "Alice", Age = 26 };

db.Users.AddRange( new []{ tom, alice });
db.SaveChanges();
Console.WriteLine("Objects saved!");
