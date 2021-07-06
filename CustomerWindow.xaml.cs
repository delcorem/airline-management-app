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
	/// Interaction logic for CustomerWindow.xaml
	/// </summary>
	public partial class CustomerWindow : Window
	{
		MainWindow main;
		public CustomerWindow(MainWindow m)
		{
			main = m;
			InitializeComponent();

			// Selects the Name from all items in the list
			var names = from cust in main.CustList
						select cust.Name;

			// Populates the listbox with the all names selected
			lstCust.DataContext = names;
		}
		
		// Populates the textboxes with information from the selected listbox item
		private void lstCust_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int i = lstCust.SelectedIndex;
			var selectedCust = from cust in main.CustList
							   where cust.ID == i
							   select cust;

			foreach(var s in selectedCust)
			{
				tbName.Text = s.Name;
				tbAddress.Text = s.Address;
				tbEmail.Text = s.Email;
				tbPhone.Text = s.Phone;
			}
		}
		// Handles the Add button, that allows the user to add an item to the list
		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			// Checks to make sure there are no empty textboxes
			if(tbName.Text == "" || tbAddress.Text == "" || tbEmail.Text == "" || tbPhone.Text == "")
			{
				MessageBox.Show("No text box can be empty!", "Error",
					MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else
			{
				// Adds the textbox information to a Customer object, then the object is added to the list
				main.CustList.Add(new Customer(main.CustList.Count, tbName.Text, tbAddress.Text,
					tbEmail.Text, tbPhone.Text));

				// Repopulates the listbox with the new list
				var names = from cust in main.CustList
							select cust.Name;
				lstCust.DataContext = names;
			}
		}
		// Handles the Update button, that allows the user to change information about a selected item in the listbox
		private void btnUpdate_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to update this customer?",
				"Update", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				// Checks to make sure no textbox is empty
				if (tbName.Text == "" || tbAddress.Text == "" || tbEmail.Text == "" || tbPhone.Text == "")
				{
					MessageBox.Show("No text box can be empty!", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}
				else
				{
					// Checks to make sure an item is selected in the listbox
					if (lstCust.SelectedIndex != -1)
					{
						// Grabs information from the textboxes, then updates the selected item with the new information
						Customer c = new Customer(lstCust.SelectedIndex, tbName.Text, tbAddress.Text,
							tbEmail.Text, tbPhone.Text);
						main.CustList[lstCust.SelectedIndex] = c;

						// Repopulates the listbox with the updated list
						var names = from cust in main.CustList
									select cust.Name;
						lstCust.DataContext = names;
					}
					else
					{
						MessageBox.Show("Must select a customer to update.", "Error",
							MessageBoxButton.OK, MessageBoxImage.Error);
					}
				}
			}
		}
		// Handles the Delete button, deleting a selected item in the listbox
		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to delete this customer?",
				"Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				// Checks to make sure there is a selected item in the listbox
				if (lstCust.SelectedIndex != -1)
				{
					// Deletes selected item
					main.CustList.RemoveAt(lstCust.SelectedIndex);

					// Resets the ID of all items remaining in the list, so there are no gaps in IDs. (Ex. 0, 1, 3, etc.)
					for (int i = 0; i < main.CustList.Count; i++)
					{
						main.CustList[i].ID = i;
					}
					// Repopulates the listbox with the new list after deletion of the selected item
					var names = from cust in main.CustList
								select cust.Name;
					lstCust.DataContext = names;
				}
				else
				{
					MessageBox.Show("Must select a customer to delete!", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
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
