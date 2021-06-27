using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DelcoreMA2
{
	/// <summary>
	/// Interaction logic for PassengerWindow.xaml
	/// </summary>
	public partial class PassengerWindow : Window
	{
		
		MainWindow main;
		public PassengerWindow(MainWindow m)
		{
			main = m;
			InitializeComponent();

			var pass = from passenger in main.PassengerStack
					   orderby passenger.ID
					   select passenger.ID;

			lstPassenger.DataContext = pass;
		}

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			if (int.TryParse(tbCustomerID.Text, out int customerID) && int.TryParse(tbFlightID.Text, out int flightID))
			{
				main.PassengerStack.Push(new Passenger(main.PassengerStack.Count, customerID,	flightID));

				var pass = from passenger in main.PassengerStack
						   orderby passenger.ID
						   select passenger.ID;

				lstPassenger.DataContext = pass;
			}
			else
			{
				MessageBox.Show("Must input a whole number for Available Seats.", "Error",
					MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btnUpdate_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to update this passenger?",
				"Update", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				if (int.TryParse(tbCustomerID.Text, out int customerID) && int.TryParse(tbFlightID.Text, out int flightID))
				{
					if (lstPassenger.SelectedIndex != -1)
					{
						// As stack does not have the functionality to update an object at a specific index,
						// I copied to a list, did the update, then converted back to a stack
						List<Passenger> passengerList = new List<Passenger>(main.PassengerStack.ToList());

						Passenger passObj = new Passenger(lstPassenger.SelectedIndex, customerID, flightID);
						passengerList[lstPassenger.SelectedIndex] = passObj;

						main.PassengerStack = new Stack<Passenger>(passengerList);

						var pass = from passenger in main.PassengerStack
								   orderby passenger.ID
								   select passenger.ID;

						lstPassenger.DataContext = pass;
					}
					else
					{
						MessageBox.Show("Must select a passenger to update.", "Error",
							MessageBoxButton.OK, MessageBoxImage.Error);
					}
				}
				else
				{
					MessageBox.Show("Must input a whole number for Customer ID and Flight ID.", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to delete this passenger?", 
				"Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				if (lstPassenger.SelectedIndex != -1)
				{
					// As stack does not have the functionality to update an object at a specific index,
					// I copied to a list, did the update, then converted back to a stack
					List<Passenger> passengerList = new List<Passenger>(main.PassengerStack.ToList());

					passengerList.RemoveAt(lstPassenger.SelectedIndex);

					for (int i = 0; i < passengerList.Count; i++)
					{
						passengerList[i].ID = i;
					}

					main.PassengerStack = new Stack<Passenger>(passengerList);

					var pass = from passenger in main.PassengerStack
							   orderby passenger.ID
							   select passenger.ID;

					lstPassenger.DataContext = pass;
				}
				else
				{
					MessageBox.Show("Must select a passenger to delete.", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		private void lstPassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int i = lstPassenger.SelectedIndex;
			var selectedPassenger = from pass in main.PassengerStack
									where pass.ID == i
									select pass;

			foreach (var s in selectedPassenger)
			{
				tbCustomerID.Text = s.CustomerID.ToString();
				tbFlightID.Text = s.FlightID.ToString();
				var cust = from c in main.CustList
						   where c.ID == s.CustomerID
						   select c.Name;
				lstCustomers.DataContext = cust;
				var flight = from f in main.FlightList
							 where f.ID == s.FlightID
							 select f.DestinationCity;
				lstFlights.DataContext = flight;
			}
		}

		private void MenuQuit_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to exit?", "Quit",
					MessageBoxButton.YesNo, MessageBoxImage.Question);
			if(result == MessageBoxResult.Yes)
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
