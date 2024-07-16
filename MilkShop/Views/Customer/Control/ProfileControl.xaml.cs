using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using MilkShop.config;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MilkShop.Views.Customer.Control
{
    /// <summary>
    /// Interaction logic for ProfileControl.xaml
    /// </summary>
    public partial class ProfileControl : UserControl
    {
        private AesEncryption encryption;
        private UserService userService;
        public ProfileControl()
        {
            InitializeComponent();
            encryption = new AesEncryption();
            userService = new UserService();
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

            var customer = Application.Current.Properties["Account"] as User;
            txt_email.Text = customer.Email;
            txt_fullName.Text = customer.FullName;
            txt_password.Password = encryption.Decrypt(customer.PasswordHash,getKey());
            txt_phone.Text = customer.PhoneNumber;
            txt_points.Text = customer.Points.ToString();
            txt_userId.Text = customer.UserId.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txt_fullName.Text;
            string email = txt_email.Text;
            string phone = txt_phone.Text;
            string points = txt_points.Text;
            string userId = txt_userId.Text;
            string password = txt_password.Password;
            string Role = "Member";

            User user = new User();
            user.Email = email;
            user.FullName = fullName;
            user.PhoneNumber = phone;
            user.UserId = Int32.Parse(userId);
            user.Role = Role;
            user.Points= Int32.Parse(points);
            user.PasswordHash = encryption.Encrypt(password,getKey());


            MessageBoxResult result = MessageBox.Show("Do you want Update?", "Comfim", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (userService.UpdateUser(user)) {
                    MessageBox.Show("Update success");
                    Application.Current.Properties["Account"] = user;
                }
                else
                {
                    MessageBox.Show("Update False");
                }
            }

            
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CustomerWindow customerWindow = new CustomerWindow();
            customerWindow.ContentArea.Content = new HomeControl(); // Navigate to HomeControl
            customerWindow.Show();
            Window.GetWindow(this).Close(); // Close current window

        }
    }
}
