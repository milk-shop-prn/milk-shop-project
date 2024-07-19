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
    /// Interaction logic for OrderDetailControl.xaml
    /// </summary>
    public partial class OrderDetailControl : Page
    {
        private IOrderDetailService orderDetailService;
        private IProductService productService;
        private IOrderService orderService;
        public OrderDetailControl()
        {
            InitializeComponent();
            orderDetailService = new OrderDetailService();
            productService = new ProductService();
            orderService = new OrderService();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string orderId = Application.Current.Properties["orderId"].ToString();
            var listOrderDetails = orderDetailService.GetOrderDetailsByOrderId(Int32.Parse(orderId));
            datag.Items.Clear();
            datag.ItemsSource = listOrderDetails;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/Customer/Control/HistoryControl.xaml", UriKind.Relative));
        }

        private void datag_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
