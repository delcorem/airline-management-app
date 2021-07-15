using System;
using System.Collections.Generic;
using System.Text;

namespace DelcoreMA2
{
	// Class that defines the Passenger object
	class Passenger
	{
		private int _ID;
		private int _customerID;
		private int _flightID;

		public Passenger(int iD, int customerID, int flightID)
		{
			ID = iD;
			CustomerID = customerID;
			FlightID = flightID;
		}

		public int ID { get => _ID; set => _ID = value; }
		public int CustomerID { get => _customerID; set => _customerID = value; }
		public int FlightID { get => _flightID; set => _flightID = value; }
	}
}
