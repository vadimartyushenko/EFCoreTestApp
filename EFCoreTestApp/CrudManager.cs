using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTestApp
{
	class CrudManager
	{
		public EfcoreappdbContext Context { get; set; }

		public CrudManager(EfcoreappdbContext context)
		{
			Context = context;
		}
		public void AddUser(BlogUser user, bool log = false)
		{

			Context.BlogUsers.Add(user);
			Context.SaveChanges();

			if (log)
				Console.WriteLine($"Объект {user} сохранен в БД");
		}

		public void UpdateUser(BlogUser user)
		{
			var dbUser = Context.BlogUsers.FirstOrDefault(x => x.UserLogin == user.UserLogin);
			
			if (dbUser == null)
				return;

			dbUser.Name = user.Name;
			dbUser.Surname = user.Surname;
			dbUser.Email = user.Email;
			dbUser.Password = user.Password;

			Context.BlogUsers.Update(dbUser);
			Context.SaveChanges();
		}

		public void DeleteUserByLogin(string login)
		{
			var dbUser = Context.BlogUsers.FirstOrDefault(x => x.UserLogin == login);

			if (dbUser == null)
				return;

			Context.BlogUsers.Remove(dbUser);
			Context.SaveChanges();
		}

		public void PrintUsers()
		{
			var users = Context.BlogUsers.ToList();

			Console.WriteLine("Blog list:");
			foreach (var i in users)
				Console.WriteLine(i);
		}
	}
}
