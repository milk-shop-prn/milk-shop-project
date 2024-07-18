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
            string orderDatailId = Application.Current.Properties["orderDetailId"].ToString();
            var listOrder = orderDetailService.GetOrderDetailById(Int32.Parse(orderDatailId));
            var product = productService.GetProductById(listOrder.ProductId);
            var order = orderService.GetOrderById(listOrder.OrderId);


            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(product.ImageUrl, UriKind.Absolute);
            bitmap.EndInit();

            txt_imgProduc.Source = bitmap;
            txt_name.Content = product.ProductName;
            txt_description.Content= product.Description;
            txt_price.Content = product.Price;
            txt_category.Content = product.Category;
            txt_orderDate.Content = order.OrderDate;

            txt_unitPrice.Content = listOrder.UnitPrice;
            txt_quantity.Content = listOrder.Quantity;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/Customer/Control/HistoryControl.xaml", UriKind.Relative));
        }
    }
}
