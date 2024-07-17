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
    /// Interaction logic for ViewCartControl.xaml
    /// </summary>
    public partial class ViewCartControl : Page
    {
        private IVoucherService voucherService;
        private decimal total_price = 0;
        private IOrderService orderService;
        private IOrderDetailService orderDetailService;
        private IUserService userService;
        private IPointService pointService;
        private IProductService productService;
        public ViewCartControl()
        {
            InitializeComponent();
            voucherService = new VoucherService();
            orderService = new OrderService();
            orderDetailService = new OrderDetailService();
            userService = new UserService();
            pointService = new PointService();
            productService = new ProductService();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var list = Application.Current.Properties["listCart"] as List<BookingProductRequest>;
            listCart.ItemsSource = list;


            total_price = list.Sum(x => x.Price * x.quality);
            txt_totalPrice.Content = total_price.ToString();
            var listVoncher1 = voucherService.GetAllVouchers();
            listVoncher.ItemsSource = listVoncher1.Where(c => c.ExpiryDate > DateTime.Now);
            var customer = Application.Current.Properties["Account"] as User;
            txt_point.Content = customer.Points.ToString() + "point";
        }

        private void listCart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var view = button.DataContext as BookingProductRequest;
            var list = Application.Current.Properties["listCart"] as List<BookingProductRequest>;

            if (list.FirstOrDefault(c => c.ProductId == view.ProductId).quality > 1)
            {
                list.FirstOrDefault(c => c.ProductId == view.ProductId).quality -= 1;
            }
            else
            {
                list.Remove(view);
            }
            listCart.ItemsSource = null;
            listCart.ItemsSource = list;
            Page_Loaded(sender, e);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var view = button.DataContext as BookingProductRequest;
            var list = Application.Current.Properties["listCart"] as List<BookingProductRequest>;
            list.FirstOrDefault(c => c.ProductId == view.ProductId).quality += 1;
            listCart.ItemsSource = null;
            listCart.ItemsSource = list;
            Page_Loaded(sender, e);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Do you want Booking", "Comfim", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var customer = Application.Current.Properties["Account"] as User;
                var list = Application.Current.Properties["listCart"] as List<BookingProductRequest>;
                Order order = new Order();
                order.TotalPrice = total_price;
                order.OrderDate = DateTime.Now;
                order.OrderStatus = "Pending";
                order.UserId = customer.UserId;

                Order order1 = orderService.GetAll().OrderByDescending(c => c.UserId).FirstOrDefault();
                orderService.SaveOrder(order);
                foreach (var item in list)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = order1.OrderId;
                    orderDetail.ProductId = item.ProductId;
                    orderDetail.Quantity = item.quality;
                    orderDetail.UnitPrice = item.Price * item.quality;

                    orderDetailService.SaveOrderDetail(orderDetail);
                }
                var listProduct = productService.GetAll();
                foreach(var item in listProduct)
                {
                    foreach(var item2 in list)
                    {
                        if(item2.ProductId == item.ProductId)
                        {
                            item.Stock -= item2.quality;
                            productService.UpdateProduct(item);
                        }
                    }
                }


                bool? isChecked = txt_point.IsChecked;
                if (isChecked == true)
                {
                    var UpdateCustomer1 = userService.GetUserById(customer.UserId);
                    UpdateCustomer1.Points = 0;
                    userService.UpdateUser(UpdateCustomer1);
                    Application.Current.Properties["Account"] = UpdateCustomer1;

                }

                


                if (listVoncher.SelectedItem is Voucher selectionVoncherId)
                {
                    int voncher_id = selectionVoncherId.VoucherId;
                    var find_voncher = voucherService.GetVoucherById(voncher_id);
                    find_voncher.IsUsed = true;
                    voucherService.UpdateVoucher(find_voncher);
                }
                BusinessObjects.Models.Point point = new BusinessObjects.Models.Point();
                point.Points = (int)((int)total_price * 0.01);
                point.UserId = customer.UserId;
                point.DateEarned = DateTime.Now;
                pointService.SavePoint(point);
                var UpdateCustomer = userService.GetUserById(customer.UserId);
                UpdateCustomer.Points +=point.Points;
                userService.UpdateUser(UpdateCustomer);
                Application.Current.Properties["Account"] = UpdateCustomer;


                Application.Current.Properties["listCart"] = new List<BookingProductRequest>();

                MessageBox.Show("Booking Success");
                NavigationService.Navigate(new Uri("Views/Customer/Control/HomeControl.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Booking false");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/Customer/Control/HomeControl.xaml", UriKind.Relative));
        }

        private void combobox_fillter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listVoncher.SelectedItem is Voucher selectionVoncherId)
            {
                int voncher_id = selectionVoncherId.VoucherId;
                var find_voncher = voucherService.GetVoucherById(voncher_id);
                total_price = (Decimal)(total_price - find_voncher.Discount);
                txt_totalPrice.Content = total_price.ToString();
            }
        }

        private void txt_point_Checked(object sender, RoutedEventArgs e)
        {
            var customer = Application.Current.Properties["Account"] as User;
            bool? isChecked = txt_point.IsChecked;
            if (isChecked == true)
            {


                Decimal.TryParse(customer.Points.ToString(), out Decimal points);
                total_price -= points;
                txt_totalPrice.Content = total_price.ToString();
            }
            else
            {
                Decimal.TryParse(customer.Points.ToString(), out Decimal points);
                total_price += points;
                txt_totalPrice.Content = total_price.ToString();
            }

        }
    }
}
