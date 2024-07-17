using BusinessObjects.Models;
using Microsoft.Extensions.Configuration;
using MilkShop.config;
using MilkShop.Views.Customer;
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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private IUserService userService;
        public RegisterWindow()
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

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string email = TxtEmail.Text;
            string passworld = TxtPassword.Password;
            string fullName = TxtFullName.Text;
            string phone = TxtPhone.Text;

            AesEncryption aesEncryption = new AesEncryption();
            User user = new User();
            user.Email = email;
            user.FullName = fullName;
            user.PhoneNumber = phone;
            user.PasswordHash = aesEncryption.Encrypt(passworld, getKey());
            user.Role = "Member";
            user.Points = 0;
            user.Deleted = false;

            try
            {
                userService.SaveUser(user);
                MessageBox.Show($"Register Success"
                       , "Success", MessageBoxButton.OK
                       , MessageBoxImage.Information);
                Application.Current.Properties["Account"] = user;
                CustomerWindow userWindow = new CustomerWindow();
                userWindow.Show();
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                MessageBox.Show($"Register False"
                       , "Error", MessageBoxButton.OK
                       , MessageBoxImage.Error);
            }


        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
