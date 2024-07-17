using BusinessObjects.Models;
using Microsoft.Extensions.Configuration;
using MilkShop.config;
using MilkShop.Views.Admin;
using MilkShop.Views.Customer;
using MilkShop.Views.Staff;
using Services;
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
using System.Windows.Shapes;

namespace MilkShop.Views.Auth
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private IUserService userService;
        public LoginWindow()
        {
            InitializeComponent();
            userService = new UserService();
        }
        private string getKey()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true).Build();
            return configuration["stringKey:key"];
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = TxtEmail.Text;
            string password = TxtPassword.Password;


            User account = userService.CheckLogin(email, password, getKey());
            if (account != null)
            {
                Application.Current.Properties["Account"] = account;
                if (account.Role.Equals("Admin"))
                {
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                }
                else if (account.Role.Equals("Member"))
                {
                    CustomerWindow userWindow = new CustomerWindow();
                    userWindow.Show();
                }
                else if (account.Role.Equals("Staff"))
                {
                    StaffWindow staffWindow = new StaffWindow();
                    staffWindow.Show();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show($"Username or password invalid"
                       , "Error", MessageBoxButton.OK
                       , MessageBoxImage.Error);
            }

        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}
