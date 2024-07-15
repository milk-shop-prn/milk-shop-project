using BusinessObjects.Models;
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
using System.Windows.Shapes;

namespace MilkShop
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

        private void btn_button_Click(object sender, RoutedEventArgs e)
        {
            string email =txt_Email.Text;
            string passworld = txt_password.Password;
            string fullName = txt_fullName.Text;
            string phone = txt_phoneNumber.Text;
            string userName =txt_userName.Text;
            AesEncryption aesEncryption = new AesEncryption();
            



           User user = new User();
            user.Username = userName;
            user.Email = email;
            user.FullName = fullName;
            user.PhoneNumber = phone;
            user.PasswordHash = aesEncryption.Encrypt(passworld,getKey());
            user.Role = "Member";
            user.Points = 0;
            try
            {
                userService.SaveUser(user);
                MessageBox.Show("Register Success");
            }
            catch (Exception ex) {
                MessageBox.Show("Register False");
            }
            
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
