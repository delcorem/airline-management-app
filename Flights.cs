using System;
using System.Collections.Generic;
using System.Text;

namespace DelcoreMA2
{
	// Class that defines the Flights object
	class Flights
	{
		private int _ID;
		private int _airlineID; //ID of Airlines class
		private string _departureCity;
		private string _destinationCity;
		private string _departureDate;
		private double _flightTime; //Number of hours

		public Flights(int iD, int airlineID, string departureCity, string destinationCity, string departureDate, double flightTime)
		{
			ID = iD;
			AirlineID = airlineID;
			DepartureCity = departureCity;
			DestinationCity = destinationCity;
			DepartureDate = departureDate;
			FlightTime = flightTime;
		}

		public int ID { get => _ID; set => _ID = value; }
		public int AirlineID { get => _airlineID; set => _airlineID = value; }
		public string DepartureCity { get => _departureCity; set => _departureCity = value; }
		public string DestinationCity { get => _destinationCity; set => _destinationCity = value; }
		public string DepartureDate { get => _departureDate; set => _departureDate = value; }
		public double FlightTime { get => _flightTime; set => _flightTime = value; }
	}
}
