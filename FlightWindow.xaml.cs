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
	/// Interaction logic for FlightWindow.xaml
	/// </summary>
	public partial class FlightWindow : Window
	{
		
		MainWindow main;
		public FlightWindow(MainWindow pMain)
		{
			main = pMain;
			
			InitializeComponent();

			// Selects the departure city information for each item
			var cities = from flight in main.FlightList
						select flight.DepartureCity;

			// Populates the listbox with the departure city for each item
			lstFlight.DataContext = cities;
		}
		// Handles Add button
		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			// Checks to make sure no textbox is empty
			if (tbAirline.Text == "" || tbDepartureCity.Text == "" || tbDestinationCity.Text == "" ||
				tbDepartureDate.Text == "" || tbFlightTime.Text == "")
			{
				MessageBox.Show("No text box can be empty.", "Error",
					MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else if (int.TryParse(tbAirline.Text, out int airlineID) &&
				double.TryParse(tbFlightTime.Text, out double flightTime))
			{
				main.FlightList.Add(new Flights(main.FlightList.Count, airlineID,
						tbDepartureCity.Text, tbDestinationCity.Text, tbDepartureDate.Text, flightTime));

				var cities = from flight in main.FlightList
							 select flight.DepartureCity;
				lstFlight.DataContext = cities;
			}
			else
			{
				MessageBox.Show("Must input a whole number for Airline ID, and a number for Number of Hours.", "Error",
					MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		// Handles Update button
		private void btnUpdate_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to update this flight?",
				"Update", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				// Checks if any textboxes are empty
				if (tbAirline.Text == "" || tbDepartureCity.Text == "" || tbDestinationCity.Text == "" ||
				tbDepartureDate.Text == "" || tbFlightTime.Text == "")
				{
					MessageBox.Show("No text box can be empty.", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}
				else if (int.TryParse(tbAirline.Text, out int airlineID) &&
					double.TryParse(tbFlightTime.Text, out double flightTime))
				{
					// Checks if an item in the listbox is selected before updating
					if (lstFlight.SelectedIndex != -1)
					{
						Flights flightObj = new Flights(lstFlight.SelectedIndex, airlineID,
							tbDepartureCity.Text, tbDestinationCity.Text, tbDepartureDate.Text, flightTime);
						main.FlightList[lstFlight.SelectedIndex] = flightObj;

						var cities = from flight in main.FlightList
									 select flight.DepartureCity;
						lstFlight.DataContext = cities;
					}
					else
					{
						MessageBox.Show("Must select a flight to update.", "Error",
							MessageBoxButton.OK, MessageBoxImage.Error);
					}
				}
				else
				{
					MessageBox.Show("Must input a whole number for Airline ID, and a number for Number of Hours.",
						"Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}
		// Handles Delete button
		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to delete this flight?",
				"Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				// Checks if an item in the listbox is selected before deletion
				if (lstFlight.SelectedIndex != -1)
				{
					main.FlightList.RemoveAt(lstFlight.SelectedIndex);

					for (int i = 0; i < main.FlightList.Count; i++)
					{
						main.FlightList[i].ID = i;
					}
					var cities = from flight in main.FlightList
								 select flight.DepartureCity;
					lstFlight.DataContext = cities;
				}
				else
				{
					MessageBox.Show("Must select a flight to delete.", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}
		// Handles population of textboxes when an item in the listbox is selected
		private void lstFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int i = lstFlight.SelectedIndex;
			var selectedFlight = from flight in main.FlightList
							   where flight.ID == i
							   select flight;

			foreach (var s in selectedFlight)
			{
				tbAirline.Text = s.AirlineID.ToString();
				tbDepartureCity.Text = s.DepartureCity;
				tbDestinationCity.Text = s.DestinationCity;
				tbDepartureDate.Text = s.DepartureDate;
				tbFlightTime.Text = s.FlightTime.ToString();
				var air = from a in main.AirlineQueue
						  where a.ID == s.AirlineID
						  select a.Name;
				lstAirlines.DataContext = air;
			}
		}
		// Closes program from File > Quit in the menu bar
		private void MenuQuit_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to exit?", "Quit",
					MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
			{
				Application.Current.Shutdown();
			}
		}
		// Opens HelpWindow from the menu bar
		private void MenuHelp_Click(object sender, RoutedEventArgs e)
		{
			HelpWindow helpWindow = new HelpWindow();
			helpWindow.ShowDialog();
		}
	}
}
