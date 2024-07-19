using BusinessObjects.Models;
using BusinessObjects.Request;
using Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MilkShop.Views.Customer.Control
{
    /// <summary>
    /// Interaction logic for HistoryControl.xaml
    /// </summary>
    public partial class HistoryControl : Page
    {
        private IOrderService orderService;
        
        public HistoryControl()
        {
            InitializeComponent();
            orderService = new OrderService();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            User user = Application.Current.Properties["Account"] as User;
            var listOrder1 = orderService.GetAll();
           
            listOrder1 = listOrder1.Where(c => c.UserId.Equals(user.UserId)).ToList();
             foreach ( var item in listOrder1)
            {
                item.User = user;
            }

            listOrder.ItemsSource = listOrder1;
        }

        private void listCart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var OrderDetail = button.DataContext as Order;
            Application.Current.Properties["orderId"] = OrderDetail.OrderId;
            NavigationService.Navigate(new Uri("Views/Customer/Control/OrderDetailControl.xaml", UriKind.Relative));

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/Customer/Control/HomeControl.xaml", UriKind.Relative));
        }
    }
}
