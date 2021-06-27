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
		// private Queue<Airlines> airlineQueue = new Queue<Airlines>();
		MainWindow main;

		private string airplaneType = "";
		private string mealChoice = "";

		public AirlineWindow(MainWindow m)
		{
			main = m;
			InitializeComponent();

			var plane = from airline in main.AirlineQueue
						 select airline.Name;

			lstAirlines.DataContext = plane;
		}

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
				switch(s.MealAvailable)
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

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			if (tbName.Text == "" || tbSeats.Text == "" || airplaneType == "" ||
				mealChoice == "")
			{
				MessageBox.Show("No text box or choice can be empty.", "Error",
					MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else if (int.TryParse(tbSeats.Text, out int seatsAvailable))
			{
				main.AirlineQueue.Enqueue(new Airlines(main.AirlineQueue.Count, tbName.Text,
						airplaneType, seatsAvailable, mealChoice));

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

		private void btnUpdate_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to update this airline?",
				"Update", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				if (tbName.Text == "" || tbSeats.Text == "" || airplaneType == "" ||
				mealChoice == "")
				{
					MessageBox.Show("No text box  or choice can be empty.", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}
				else if (int.TryParse(tbSeats.Text, out int seatsAvailable))
				{
					if (lstAirlines.SelectedIndex != -1)
					{
						// As queue does not have the functionality to update an object at a specific index,
						// I copied to a list, did the update, then converted back to a queue
						List<Airlines> airlineList = new List<Airlines>(main.AirlineQueue.ToList());

						Airlines airObj = new Airlines(lstAirlines.SelectedIndex, tbName.Text,
							airplaneType, seatsAvailable, mealChoice);
						airlineList[lstAirlines.SelectedIndex] = airObj;

						main.AirlineQueue = new Queue<Airlines>(airlineList);

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

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to delete this airline?",
				"Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				if (lstAirlines.SelectedIndex != -1)
				{
					// As queues don't have the functionality to delete at a specific index, I convert to
					// a list, delete at specified index, then convert back to a queue
					List<Airlines> airlineList = new List<Airlines>(main.AirlineQueue.ToList());

					airlineList.RemoveAt(lstAirlines.SelectedIndex);

					for (int i = 0; i < airlineList.Count; i++)
					{
						airlineList[i].ID = i;
					}

					main.AirlineQueue = new Queue<Airlines>(airlineList);

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
