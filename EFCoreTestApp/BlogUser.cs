using System;
using System.Collections.Generic;

#nullable disable

namespace EFCoreTestApp
{
    public class BlogUser
    {
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }

        public override string ToString()
        {
	        return $"User {Name} {Patronymic} {Surname} (login: {UserLogin}) registered at {RegistrationDate}";
        }
  }
}
