using System;
using System.IO;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCoreTestApp
{
	class Program
	{
		static void Main()
		{
			var builder = new ConfigurationBuilder();
			// установка пути к текущему каталогу
			builder.SetBasePath(Directory.GetCurrentDirectory());
			builder.AddJsonFile("appsettings.json");

			var config = builder.Build();
			var connectionStr = config.GetConnectionString("DefaultConnection");

			var optionsBuilder = new DbContextOptionsBuilder<EfcoreappdbContext>();
			var options = optionsBuilder.UseSqlServer(connectionStr).Options;

			using (var db = new EfcoreappdbContext(options))
			{

				var manager = new CrudManager(db);
				manager.PrintUsers();

				Console.WriteLine("----------------------------------------------");

				/*var user = new BlogUser()
				{
					UserLogin = "ivashka$12",
					Name = "Pavel",
					Surname = "Ivanov",
					Email = "p.newivanov@ya.ru",
					Password = "goierew_)_ewwer",
				};

				manager.DeleteUserByLogin(user.UserLogin);

				manager.PrintUsers();*/
			}

			Console.ReadKey();
		}
	}
}
