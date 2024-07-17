using BusinessObjects.Models;
using BusinessObjects.Request;
using MilkShop.Views.Auth;
using MilkShop.Views.Customer.Control;
using System;
using System.Collections;
using System.Collections.Generic;
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

namespace MilkShop.Views.Customer
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {

        private List<BookingProductRequest> listCart = new List<BookingProductRequest>();
        public CustomerWindow()
        {
            InitializeComponent();
            LV.SelectedIndex = 0;
            Application.Current.Properties["listCart"] = listCart;
        }

        private void LV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LV.SelectedItem is ListViewItem selectedItem)
            {
                switch (selectedItem.Tag.ToString())
                {
                    case "Home":
                        ContentArea.NavigationService.Navigate(new Uri("Views/Customer/Control/HomeControl.xaml", UriKind.Relative));
                        break;
                    case "Profile":
                        ContentArea.Content = new ProfileControl();
                        break;
                    case "Blog":
                        ContentArea.Content = new BlogControl();
                        break;
                    case "History":
                        ContentArea.Content = new HistoryControl();
                        break;
                    case "SignOut":
                        Application.Current.Properties["Account"] = null;
                        LoginWindow login = new LoginWindow();
                        login.Show();
                        this.Close();
                        break;
                }
            }
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_profile.Visibility = Visibility.Collapsed;
                tt_history.Visibility = Visibility.Collapsed;
                tt_signout.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_profile.Visibility = Visibility.Visible;
                tt_history.Visibility = Visibility.Visible;
                tt_signout.Visibility = Visibility.Visible;
            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 0.3;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {

        }




        //private void CloseBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    Application.Current.Shutdown();
        //}

        //private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    WindowState = WindowState.Minimized;
        //}


    }
}
