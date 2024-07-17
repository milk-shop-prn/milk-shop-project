using BusinessObjects.Models;
using Microsoft.Extensions.Configuration;
using MilkShop.config;
using Repositories;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace MilkShop.Views.Staff.Control
{
	/// <summary>
	/// Interaction logic for UserManagementControl.xaml
	/// </summary>
	public partial class UserManagementControl : UserControl
	{
		IUserRepository _repo = new UserRepository();
		bool NewOrUpdate = false;
		public UserManagementControl()
		{
			InitializeComponent();
		}

		private string getKey()
		{
			IConfiguration configuration = new ConfigurationBuilder()
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("appsettings.json", true, true).Build();
			return configuration["stringKey:key"];
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			LoadUser();
		}

		private void LoadUser()
		{
			var listUser = _repo.GetAll();
			dbUser.ItemsSource = listUser;
		}

		private void DbUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (dbUser.SelectedItem is User selectedUser)
			{
				AesEncryption aesEncryption = new AesEncryption();
				string decryptedPassword = aesEncryption.Decrypt(selectedUser.PasswordHash, getKey());
				txtPassword.Text = decryptedPassword;
				txtUserID.Text = selectedUser.UserId.ToString();
				txtFullname.Text = selectedUser.FullName;
				txtEmail.Text = selectedUser.Email;
				txtPhonenumber.Text = selectedUser.PhoneNumber;
				txtRole.Text = selectedUser.Role;
				txtPoint.Text = selectedUser.Points.ToString();
			}
			else
			{
				txtPassword.Text = string.Empty;
				txtUserID.Text = string.Empty;
				txtFullname.Text = string.Empty;
				txtEmail.Text = string.Empty;
				txtPhonenumber.Text = string.Empty;
				txtRole.Text = string.Empty;
				txtPoint.Text = string.Empty;
			}
		}

		private void ClearTextbox()
		{
			txtPassword.Text = string.Empty;
			txtUserID.Text = string.Empty;
			txtFullname.Text = string.Empty;
			txtEmail.Text = string.Empty;
			txtPhonenumber.Text = string.Empty;
			txtRole.Text = string.Empty;
			txtPoint.Text = string.Empty;
		}

		private void EnableText(bool status)
		{
			txtPassword.IsEnabled = status;
			txtFullname.IsEnabled = status;
			txtEmail.IsEnabled = status;
			txtPhonenumber.IsEnabled = status;
			txtRole.IsEnabled = status;
			txtPoint.IsEnabled = status;
		}

		private void btnNew_Click(object sender, RoutedEventArgs e)
		{
			if(btnNew.Content.ToString() == "New")
			{
				btnNew.Content = "Cancel";
				btnSave.IsEnabled = true;
				btnUpdate.IsEnabled = false;
				NewOrUpdate = true;

				ClearTextbox();
				EnableText(true);
			}else if(btnNew.Content.ToString() == "Cancel")
			{
				btnNew.Content = "New";
				btnSave.IsEnabled = false;
				btnUpdate.IsEnabled = true;
				NewOrUpdate = false;

				EnableText(false);
			}
		}

		private void btnUpdate_Click(object sender, RoutedEventArgs e)
		{
			if (dbUser.SelectedItem is User selectedUser)
			{
				if (btnUpdate.Content.ToString() == "Update")
				{
					EnableText(true);
					txtUserID.IsEnabled = false;

					btnUpdate.Content = "Cancel";
					btnSave.IsEnabled = true;
					NewOrUpdate = false;
				}
				else
				{
					EnableText(false);
					btnUpdate.Content = "Update";
					btnSave.IsEnabled = false;
				}
			}
			else
			{
				MessageBox.Show("Please select a user to update", "User Management System",
							MessageBoxButton.OK
						   , MessageBoxImage.Information);
			}
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			AesEncryption aesEncryption = new AesEncryption();

			if (txtFullname.Text.Trim() == "" || txtEmail.Text.Trim() == "" ||
				txtPhonenumber.Text.Trim() == "" || txtRole.Text.Trim() == "" ||
				txtPoint.Text.Trim() == "" || txtPassword.Text.Trim() == "")
			{
				MessageBox.Show("All fields required."
						   , "User Management System", MessageBoxButton.OK
						   , MessageBoxImage.Information);
			}
			else
			{
				var user = new User
				{
					FullName = txtFullname.Text,
					Email = txtEmail.Text,
					PhoneNumber = txtPhonenumber.Text,
					Role = txtRole.Text,
					Points = int.Parse(txtPoint.Text),
					PasswordHash = aesEncryption.Encrypt(txtPassword.Text, getKey()),
				};

                if (NewOrUpdate)
                {
					_repo.SaveUser(user);
					MessageBox.Show("New user added."
						   , "Success", MessageBoxButton.OK
						   , MessageBoxImage.Information);
					btnNew.Content = "New";
				}
				else
				{
					_repo.UpdateUser(user);
					btnUpdate.Content = "Update";
				}

				EnableText(false);
				btnSave.IsEnabled = false;
				LoadUser();
            }
		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			if(dbUser.SelectedItem is User selectedUser)
			{
				MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this user?",
					"Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
				if(result == MessageBoxResult.Yes)
				{
					_repo.DeleteUser(selectedUser);
					LoadUser();
				}
				
			}
			else
			{
				MessageBox.Show("Please select a user to delete."
					   , "Warning", MessageBoxButton.OK
					   , MessageBoxImage.Warning);
			}
		}
	}
}
