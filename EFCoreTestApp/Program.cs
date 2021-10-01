using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTestApp
{
	class Program
	{
		static void Main()
		{
			using (var db = new EfcoreappdbContext())
			{

				var manager = new CrudManager(db);
				manager.PrintUsers();

				Console.WriteLine("----------------------------------------------");

				var user = new BlogUser()
				{
					UserLogin = "ivashka$12",
					Name = "Pavel",
					Surname = "Ivanov",
					Email = "p.newivanov@ya.ru",
					Password = "goierew_)_ewwer",
				};

				manager.DeleteUserByLogin(user.UserLogin);

				manager.PrintUsers();
			}

			Console.ReadKey();
		}
	}
}
