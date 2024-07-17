using BusinessObjects.Models;
using DataAccessObjects;
using DTO.Request;
using Microsoft.IdentityModel.Tokens;
using Services;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using Image = System.Windows.Controls.Image;

namespace MilkShop.Views.Customer.Control
{
    /// <summary>
    /// Interaction logic for HomeControl.xaml
    /// </summary>
    public partial class HomeControl : Page
    {
        private IProductService productService;
        public event EventHandler ShowProductDetail;
        private IOrderService orderService;
        public HomeControl()
        {
            InitializeComponent();
            productService = new ProductService();
            orderService = new OrderService();  
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<Product> ListProducts = productService.GetAll();
            ProductsItemsControl.ItemsSource=ListProducts;
            var list = Application.Current.Properties["listCart"] as List<BookingProductRequest>;
            if(list != null)
            {
                txt_change.Text = list.Count().ToString();
            }
            else
            {
                txt_change.Text = "0";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txt_find.Focus();
        }

        private void txt_find_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void txt_find_TextChanged(object sender, TextChangedEventArgs e)
        {
            string findByName = txt_find.Text;
            if (!string.IsNullOrEmpty(txt_find.Text) && txt_find.Text.Length > 0)
            {
                text_find.Visibility = Visibility.Collapsed;

                List<Product> findListProduc = productService.GetAll();
                findListProduc = findListProduc.Where(x => x.ProductName.Contains(findByName)).ToList();
                ProductsItemsControl.ItemsSource=findListProduc;
            }
            else
            {
                text_find.Visibility = Visibility.Visible;
                List<Product> findListProduc = productService.GetAll();
                ProductsItemsControl.ItemsSource = findListProduc;
            }
          
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && button.CommandParameter != null)
            {
                int productId = (int)button.CommandParameter;
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
                        bookingProductRequest.urlImg = product.ImageUrl;
                        listProducts.Add(bookingProductRequest);
                    }
                    
                    Application.Current.Properties["listCart"] = listProducts;
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
                    bookingProductRequest.urlImg = product.ImageUrl;
                    listProducts.Add(bookingProductRequest);
                    Application.Current.Properties["listCart"] = listProducts;
                }

               

            }
            UserControl_Loaded(sender, e);


        }



        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
           
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;


            if (button != null && button.CommandParameter != null)
            {
                int productId = (int)button.CommandParameter;
                Application.Current.Properties["ProductId"] = productId;
                NavigationService.Navigate(new Uri("Views/Customer/Control/ViewDetail.xaml", UriKind.Relative));
            }
            

        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("Views/Customer/Control/ViewCartControl.xaml", UriKind.Relative));
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            ScaleTransform scale = (ScaleTransform)((Image)sender).RenderTransform;
            scale.ScaleX = 0.8;
            scale.ScaleY = 0.8;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            ScaleTransform scale = (ScaleTransform)((Image)sender).RenderTransform;
            scale.ScaleX = 1;
            scale.ScaleY = 1;
        }

        private void txt_change_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }
    }
}
