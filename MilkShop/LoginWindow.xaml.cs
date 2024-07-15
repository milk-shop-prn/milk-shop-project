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
using System.Windows.Shapes;

namespace MilkShop
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

        private void btn_button_Click(object sender, RoutedEventArgs e)
        {
            string email = txt_Email.Text;
            string password = txt_password.Password;


            AesEncryption aesEncryption = new AesEncryption();
            User account = userService.GetAll().Where(c => c.Email.Equals(email) && aesEncryption.Decrypt(c.PasswordHash,getKey()).Equals(password)).First();
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
                    UserWindow userWindow = new UserWindow();
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
                MessageBox.Show("userName or password invalid");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Hide();
        }
    }
}
