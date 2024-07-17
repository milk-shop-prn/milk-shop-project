using BusinessObjects.Models;
using BusinessObjects.Request;
using Microsoft.IdentityModel.Tokens;
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
    /// Interaction logic for ViewDetail.xaml
    /// </summary>
    public partial class ViewDetail : Page
    {
        private IProductService productService;
        public ViewDetail()
        {
            InitializeComponent();
            productService = new ProductService();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/Customer/Control/HomeControl.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {


            int productId = Int32.Parse(txt_productId.Content.ToString());
            var product = productService.GetProductById(productId);

            List<BookingProductRequest> listProducts = Application.Current.Properties["listCart"] as List<BookingProductRequest>;

            if (!listProducts.IsNullOrEmpty())
            {
                var findBooking = listProducts.FirstOrDefault(x => x.ProductId == productId);
                if (findBooking != null)
                {
                    findBooking.quality += 1;
                }
                else
                {
                    BookingProductRequest bookingProductRequest = new BookingProductRequest();
                    bookingProductRequest.ProductId = product.ProductId;
                    bookingProductRequest.ProductName = product.ProductName;
                    bookingProductRequest.Price = product.Price;
                    bookingProductRequest.Stock = product.Stock;
                    bookingProductRequest.Description = product.Description;
                    bookingProductRequest.Category = product.Category;
                    bookingProductRequest.quality = 1;
                    listProducts.Add(bookingProductRequest);
                }
                MessageBox.Show(listProducts.Count.ToString() + " show khac nul");
                Application.Current.Properties["listCart"] = listProducts;
            }
            else
            {
                MessageBox.Show(listProducts.Count.ToString() + " show  nul");

                BookingProductRequest bookingProductRequest = new BookingProductRequest();
                bookingProductRequest.ProductId = product.ProductId;
                bookingProductRequest.ProductName = product.ProductName;
                bookingProductRequest.Price = product.Price;
                bookingProductRequest.Stock = product.Stock;
                bookingProductRequest.Description = product.Description;
                bookingProductRequest.Category = product.Category;
                bookingProductRequest.quality = 1;
                listProducts.Add(bookingProductRequest);
                Application.Current.Properties["listCart"] = listProducts;
            }

            NavigationService.Navigate(new Uri("Views/Customer/Control/HomeControl.xaml", UriKind.Relative));

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string productId = Application.Current.Properties["ProductId"].ToString();
            var findProduct = productService.GetProductById(Int32.Parse(productId));
            txt_productId.Content = findProduct.ProductId;
            txt_productName.Content = findProduct.ProductName;
            txt_price.Content = findProduct.Price;
            txt_description.Text = findProduct.Description;
            txt_stock.Content = findProduct.Stock;
            txt_category.Content = findProduct.Category;
            txtImg.Source = new BitmapImage(new Uri(findProduct.ImageUrl));

        }
    }
}
