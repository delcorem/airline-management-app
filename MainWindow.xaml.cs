using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DelcoreMA2
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private List<Flights> flightList = new List<Flights>();
		private Queue<Airlines> airlineQueue = new Queue<Airlines>();
		private Stack<Passenger> passengerStack = new Stack<Passenger>();
		private List<Customer> custList = new List<Customer>();

		internal List<Flights> FlightList { get => flightList; set => flightList = value; }
		internal Queue<Airlines> AirlineQueue { get => airlineQueue; set => airlineQueue = value; }
		internal Stack<Passenger> PassengerStack { get => passengerStack; set => passengerStack = value; }
		internal List<Customer> CustList { get => custList; set => custList = value; }


		public MainWindow()
		{
			InitializeComponent();

			// Prepopulating Customer List
			CustList.Add(new Customer(0, "Kyle Crane", "12 GRE Road", "craneky@GRE.com", "(423)181-0001"));
			CustList.Add(new Customer(1, "Brecken", "124 Tower Ave", "towerman@Tower.com", "(423)133-0024"));
			CustList.Add(new Customer(2, "Gazi", "111 Harran Road", "gazi@har.com", "(423)181-5431"));
			CustList.Add(new Customer(3, "Orcan", "144 Inner City Road", "brother@mother.com", "(423)181-4441"));
			CustList.Add(new Customer(4, "Jade", "444 Parkour Road", "jade@har.com", "(423)181-1234"));

			// Prepopulating Flights List
			FlightList.Add(new Flights(0, 0, "Toronto", "Vancouver", "July 1, 2021", 3.33));
			FlightList.Add(new Flights(1, 1, "Vancouver", "Kamloops", "July 13, 2021", 1.12));
			FlightList.Add(new Flights(2, 2, "Saskatoon", "New York", "July 24, 2021", 3.82));
			FlightList.Add(new Flights(3, 3, "Tokyo", "Toronto", "July 3, 2021", 16.15));
			FlightList.Add(new Flights(4, 4, "Regina", "Yellowknife", "July 27, 2021", 4.25));

			// Prepopulating Airlines Queue
			AirlineQueue.Enqueue(new Airlines(0, "Air Canada", "Boeing 777", 40, "Pizza"));
			AirlineQueue.Enqueue(new Airlines(1, "Qatar Airways", "Airbus 220", 22, "Sushi"));
			AirlineQueue.Enqueue(new Airlines(2, "Singapore Airlines", "Lockheed 120", 33, "Chicken"));
			AirlineQueue.Enqueue(new Airlines(3, "Harran Airlines", "Comac C919", 15, "Burger"));
			AirlineQueue.Enqueue(new Airlines(4, "Scottish Airways", "Douglas DC6", 50, "Haggis"));

			// Prepopulating Passengers Stack
			PassengerStack.Push(new Passenger(0, 0, 0));
			PassengerStack.Push(new Passenger(1, 1, 1));
			PassengerStack.Push(new Passenger(2, 2, 2));
			PassengerStack.Push(new Passenger(3, 3, 3));
			PassengerStack.Push(new Passenger(4, 4, 4));
		}

		private void btnCustomer_Click(object sender, RoutedEventArgs e)
		{
			CustomerWindow customerWindow = new CustomerWindow(this);
			customerWindow.ShowDialog();
		}

		private void btnFlight_Click(object sender, RoutedEventArgs e)
		{
			FlightWindow flightWindow = new FlightWindow(this);
			flightWindow.ShowDialog();
		}

		private void btnAirline_Click(object sender, RoutedEventArgs e)
		{
			AirlineWindow airlineWindow = new AirlineWindow(this);
			airlineWindow.ShowDialog();
		}

		private void btnPassenger_Click(object sender, RoutedEventArgs e)
		{
			PassengerWindow passengerWindow = new PassengerWindow(this);
			passengerWindow.ShowDialog();
		}

		private void MenuQuit_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to exit?", "Quit",
					MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
			{
				Application.Current.Shutdown();
			}
		}

		private void MenuHelp_Click(object sender, RoutedEventArgs e)
		{
			HelpWindow helpWindow = new HelpWindow();
			helpWindow.ShowDialog();
		}
	}
}
