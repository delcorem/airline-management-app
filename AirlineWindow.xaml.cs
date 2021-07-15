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
	/// Interaction logic for AirlineWindow.xaml
	/// </summary>
	public partial class AirlineWindow : Window
	{
		// Reference to MainWindow to use AirlineQueue as per requirements
		MainWindow main;

		private string airplaneType = "";
		private string mealChoice = "";

		public AirlineWindow(MainWindow m)
		{
			main = m;
			InitializeComponent();

			// Populates the listbox with the Name from each item in the stack
			var plane = from airline in main.AirlineQueue
						 select airline.Name;
			lstAirlines.DataContext = plane;
		}
		// Populates the textboxes and radio buttons with information from the selected listbox item
		private void lstAirlines_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int i = lstAirlines.SelectedIndex;
			var selectedAirline = from air in main.AirlineQueue
								 where air.ID == i
								 select air;

			foreach (var s in selectedAirline)
			{
				tbName.Text = s.Name;
				tbSeats.Text = s.SeatsAvailable.ToString();
				// Checks a radio button depending on the Airplane in the queue object
				switch(s.Airplane)
				{
					case "Boeing 777":
						rbBoeing.IsChecked = true;
						break;
					case "Airbus 220":
						rbAirbus.IsChecked = true;
						break;
					case "Lockheed 120":
						rbLockheed.IsChecked = true;
						break;
					case "Comac C919":
						rbComac.IsChecked = true;
						break;
					default:
						rbDouglas.IsChecked = true;
						break;
				}
				// Checks a radio button depending on the MealAvailable in the queue object
				switch (s.MealAvailable)
				{
					case "Pizza":
						rbPizza.IsChecked = true;
						break;
					case "Sushi":
						rbSushi.IsChecked = true;
						break;
					case "Chicken":
						rbChicken.IsChecked = true;
						break;
					case "Burger":
						rbBurger.IsChecked = true;
						break;
					default:
						rbHaggis.IsChecked = true;
						break;
				}

			}
		}
		// Handles the Add button, that allows the user to add an item to the queue
		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			// Checks for empty textboxes
			if (tbName.Text == "" || tbSeats.Text == "" || airplaneType == "" ||
				mealChoice == "")
			{
				MessageBox.Show("No text box or choice can be empty.", "Error",
					MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else if (int.TryParse(tbSeats.Text, out int seatsAvailable))
			{
				// Adds the textbox and radio buttpn information to a Customer object, then object is added to the queue
				main.AirlineQueue.Enqueue(new Airlines(main.AirlineQueue.Count, tbName.Text,
						airplaneType, seatsAvailable, mealChoice));

				// Repopulates the listbox with the updated queue
				var plane = from airline in main.AirlineQueue
							select airline.Name;
				lstAirlines.DataContext = plane;
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
			var result = MessageBox.Show("Are you sure you want to update this airline?",
				"Update", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				// Checks to make sure no textbox is empty
				if (tbName.Text == "" || tbSeats.Text == "" || airplaneType == "" ||
				mealChoice == "")
				{
					MessageBox.Show("No text box  or choice can be empty.", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}
				else if (int.TryParse(tbSeats.Text, out int seatsAvailable))
				{
					// Checks to make sure an item is selected in the listbox
					if (lstAirlines.SelectedIndex != -1)
					{
						/* Using a queue was a requirement, so as queues do not have the
						 * functionality to update an object at a specific index, I copied
						 * to a list, did the update, then converted back to a queue
						 */
						List<Airlines> airlineList = new List<Airlines>(main.AirlineQueue.ToList());

						// Grabs information from the textboxes, then updates the selected item with the new information
						Airlines airObj = new Airlines(lstAirlines.SelectedIndex, tbName.Text,
							airplaneType, seatsAvailable, mealChoice);
						airlineList[lstAirlines.SelectedIndex] = airObj;

						// Converts back to a queue
						main.AirlineQueue = new Queue<Airlines>(airlineList);

						// Repopulates the listbox with the updated stack
						var plane = from airline in main.AirlineQueue
									select airline.Name;
						lstAirlines.DataContext = plane;
					}
					else
					{
						MessageBox.Show("Must select an airline to update.", "Error",
							MessageBoxButton.OK, MessageBoxImage.Error);
					}
				}
				else
				{
					MessageBox.Show("Must input a whole number for Seats Available.", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}
		// Handles the Delete button, deleting a selected item in the listbox
		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to delete this airline?",
				"Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				// Checks to make sure there is a selected item in the listbox
				if (lstAirlines.SelectedIndex != -1)
				{
					/* Using a queue was a requirement, so as queues do not have the
					 * functionality to delete an object at a specific index, I copied
					 * to a list, did the delete, then converted back to a queue
					 */
					List<Airlines> airlineList = new List<Airlines>(main.AirlineQueue.ToList());

					// Deletes selected item
					airlineList.RemoveAt(lstAirlines.SelectedIndex);

					// Resets the ID of all items remaining in the list, so there are no gaps in IDs. (Ex. 0, 1, 3, etc.)
					for (int i = 0; i < airlineList.Count; i++)
					{
						airlineList[i].ID = i;
					}

					//Converts back to a queue
					main.AirlineQueue = new Queue<Airlines>(airlineList);

					// Repopulates the listbox with the updated stack
					var plane = from airline in main.AirlineQueue
								select airline.Name;
					lstAirlines.DataContext = plane;
				}
				else
				{
					MessageBox.Show("Must select a flight to delete.", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}
		// Assigns airplaneType based on the radio button clicked
		private void rbBoeing_Checked(object sender, RoutedEventArgs e)
		{
			airplaneType = "Boeing 777";
		}
		private void rbAirbus_Checked(object sender, RoutedEventArgs e)
		{
			airplaneType = "Airbus 220";
		}
		private void rbLockheed_Checked(object sender, RoutedEventArgs e)
		{
			airplaneType = "Lockheed 120";
		}
		private void rbComac_Checked(object sender, RoutedEventArgs e)
		{
			airplaneType = "Comac C919";
		}
		private void rbDouglas_Checked(object sender, RoutedEventArgs e)
		{
			airplaneType = "Douglas DC6";
		}
		// Assigns mealChoice based on the clicked radio button
		private void rbPizza_Checked(object sender, RoutedEventArgs e)
		{
			mealChoice = "Pizza";
		}
		private void rbSushi_Checked(object sender, RoutedEventArgs e)
		{
			mealChoice = "Sushi";
		}
		private void rbChicken_Checked(object sender, RoutedEventArgs e)
		{
			mealChoice = "Chicken";
		}
		private void rbBurger_Checked(object sender, RoutedEventArgs e)
		{
			mealChoice = "Burger";
		}
		private void rbHaggis_Checked(object sender, RoutedEventArgs e)
		{
			mealChoice = "Haggis";
		}
		// Closes application from the File > Quit menu item
		private void MenuQuit_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to exit?", "Quit",
					MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
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
