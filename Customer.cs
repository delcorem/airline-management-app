using System;
using System.Collections.Generic;
using System.Text;

namespace DelcoreMA2
{
	// Class that defines the Customer object
	class Customer
	{
		private int _ID;
		private string _name;
		private string _address;
		private string _email;
		private string _phone;

		public int ID { get => _ID; set => _ID = value; }
		public string Name { get => _name; set => _name = value; }
		public string Address { get => _address; set => _address = value; }
		public string Email { get => _email; set => _email = value; }
		public string Phone { get => _phone; set => _phone = value; }

		public Customer(int iD, string name, string address, string email, string phone)
		{
			ID = iD;
			Name = name;
			Address = address;
			Email = email;
			Phone = phone;
		}
	}
}
