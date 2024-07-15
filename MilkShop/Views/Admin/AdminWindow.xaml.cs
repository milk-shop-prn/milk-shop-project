using MilkShop.Views.Admin.Control;
using MilkShop.Views.Auth;
using MilkShop.Views.Staff.Control;
using System;
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

namespace MilkShop.Views.Admin
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            LV.SelectedIndex = 0;
        }
        private void LV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LV.SelectedItem is ListViewItem selectedItem)
            {
                switch (selectedItem.Tag.ToString())
                {
                    case "Dashboard":
                        ContentArea.Content = new DashboardControl();
                        break;
                    case "Product":
                        ContentArea.Content = new ProductManagementControl();
                        break;
                    case "User":
                        ContentArea.Content = new UserManagementControl();
                        break;
                    case "Blog":
                        ContentArea.Content = new BlogManagementControl();
                        break;
                    case "Order":
                        ContentArea.Content = new OrderManagementControl();
                        break;
                    case "SignOut":
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
                tt_dashboard.Visibility = Visibility.Collapsed;
                tt_product.Visibility = Visibility.Collapsed;
                tt_user.Visibility = Visibility.Collapsed;
                tt_blog.Visibility = Visibility.Collapsed;
                tt_order.Visibility = Visibility.Collapsed;
                tt_signout.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_dashboard.Visibility = Visibility.Visible;
                tt_product.Visibility = Visibility.Visible;
                tt_user.Visibility = Visibility.Visible;
                tt_blog.Visibility = Visibility.Visible;
                tt_order.Visibility = Visibility.Visible;
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
    }

}
