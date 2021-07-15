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
		// Reference to MainWindow to use PassengerStack as per requirements
		MainWindow main;
		public PassengerWindow(MainWindow m)
		{
			main = m;
			InitializeComponent();

			// Populates the listbox with the stack
			var pass = from passenger in main.PassengerStack
					   orderby passenger.ID
					   select passenger.ID;
			lstPassenger.DataContext = pass;
		}
		// Handles the Add button, that allows the user to add an item to the stack
		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			// Converts the text inside the textboxes to int, and checks to make sure it converted correctly
			if (int.TryParse(tbCustomerID.Text, out int customerID) && int.TryParse(tbFlightID.Text, out int flightID))
			{
				// Adds the textbox information to a Customer object, then the object is added to the stack
				main.PassengerStack.Push(new Passenger(main.PassengerStack.Count, customerID,	flightID));

				// Repopulates the listbox with the updated stack
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
		// Handles the Update button, that allows the user to change information about a selected item in the listbox
		private void btnUpdate_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to update this passenger?",
				"Update", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				// Converts the text inside the textboxes to int, and checks to make sure it converted correctly
				if (int.TryParse(tbCustomerID.Text, out int customerID) && int.TryParse(tbFlightID.Text, out int flightID))
				{
					// Checks to make sure an item is selected in the listbox
					if (lstPassenger.SelectedIndex != -1)
					{
						/* Using a stack was a requirement, so as stacks do not have the
						 * functionality to update an object at a specific index, I copied
						 * to a list, did the update, then converted back to a stack
						 */
						List<Passenger> passengerList = new List<Passenger>(main.PassengerStack.ToList());

						// Grabs information from the textboxes, then updates the selected item with the new information
						Passenger passObj = new Passenger(lstPassenger.SelectedIndex, customerID, flightID);
						passengerList[lstPassenger.SelectedIndex] = passObj;

						// Converts back to a stack
						main.PassengerStack = new Stack<Passenger>(passengerList);

						// Repopulates the listbox with the updated stack
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
		// Handles the Delete button, deleting a selected item in the listbox
		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to delete this passenger?", 
				"Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				// Checks to make sure there is a selected item in the listbox
				if (lstPassenger.SelectedIndex != -1)
				{
					/* Using a stack was a requirement, so as stacks do not have the
					 * functionality to delete an object at a specific index, I copied
					 * to a list, did the delete, then converted back to a stack
					 */
					List<Passenger> passengerList = new List<Passenger>(main.PassengerStack.ToList());

					// Deletes selected item
					passengerList.RemoveAt(lstPassenger.SelectedIndex);

					// Resets the ID of all items remaining in the list, so there are no gaps in IDs. (Ex. 0, 1, 3, etc.)
					for (int i = 0; i < passengerList.Count; i++)
					{
						passengerList[i].ID = i;
					}

					// Converts back to stack
					main.PassengerStack = new Stack<Passenger>(passengerList);

					// Repopulates the listbox with the updated stack
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
		// Populates the textboxes with information from the selected listbox item
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
		// Closes application from the File > Quit menu item
		private void MenuQuit_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to exit?", "Quit",
					MessageBoxButton.YesNo, MessageBoxImage.Question);
			if(result == MessageBoxResult.Yes)
			{
				Application.Current.Shutdown();
			}
		}
		// Opens HelpWindow from the Help menu item
		private void MenuHelp_Click(object sender, RoutedEventArgs e)
		{
			HelpWindow helpWindow = new HelpWindow();
			helpWindow.ShowDialog();
		}
	}
}
