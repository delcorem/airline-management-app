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

			var names = from cust in main.CustList
						select cust.Name;

			lstCust.DataContext = names;
		}

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

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			if(tbName.Text == "" || tbAddress.Text == "" || tbEmail.Text == "" || tbPhone.Text == "")
			{
				MessageBox.Show("No text box can be empty!", "Error",
					MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else
			{
				main.CustList.Add(new Customer(main.CustList.Count, tbName.Text, tbAddress.Text,
					tbEmail.Text, tbPhone.Text));

				var names = from cust in main.CustList
							select cust.Name;
				lstCust.DataContext = names;
			}
		}

		private void btnUpdate_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to update this customer?",
				"Update", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				if (tbName.Text == "" || tbAddress.Text == "" || tbEmail.Text == "" || tbPhone.Text == "")
				{
					MessageBox.Show("No text box can be empty!", "Error",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}
				else
				{
					if (lstCust.SelectedIndex != -1)
					{
						Customer c = new Customer(lstCust.SelectedIndex, tbName.Text, tbAddress.Text,
							tbEmail.Text, tbPhone.Text);
						main.CustList[lstCust.SelectedIndex] = c;

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

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to delete this customer?",
				"Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				if (lstCust.SelectedIndex != -1)
				{
					main.CustList.RemoveAt(lstCust.SelectedIndex);

					for (int i = 0; i < main.CustList.Count; i++)
					{
						main.CustList[i].ID = i;
					}
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
