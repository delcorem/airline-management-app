using System;
using System.Collections.Generic;
using System.Text;

namespace DelcoreMA2
{
	// Class that defines the Logins object
	class Logins
	{
		private int _ID;
		private string _username;
		private string _password;
		private int _superuser;

		public int ID { get => _ID; set => _ID = value; }
		public string Username { get => _username; set => _username = value; }
		public string Password { get => _password; set => _password = value; }
		public int Superuser { get => _superuser; set => _superuser = value; }

		public Logins(int iD, string username, string password, int superuser)
		{
			ID = iD;
			Username = username;
			Password = password;
			Superuser = superuser;
		}
	}
}
