using System;
using System.Collections.Generic;
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
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		// Temporary username and password
		private string user = "";
		private string pass = "";

		public LoginWindow()
		{
			InitializeComponent();
		}
		// Handles Login button
		private void btnLogin_Click(object sender, RoutedEventArgs e)
		{
			// Checks to make sure entered username and password match with stored username and password
			if((user == tbUser.Text) && (pass == pbPass.Password))
			{
				// Opens MainWindow
				MainWindow m = new MainWindow();
				m.Title = "Welcome";
				m.ShowDialog();
			}
			else
			{
				MessageBox.Show("Incorrect Username or Password", "Login Error",
					MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
