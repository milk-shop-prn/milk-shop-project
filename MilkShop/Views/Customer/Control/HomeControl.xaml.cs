using BusinessObjects.Models;
using DataAccessObjects;
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

namespace MilkShop.Views.Customer.Control
{
    /// <summary>
    /// Interaction logic for HomeControl.xaml
    /// </summary>
    public partial class HomeControl : UserControl
    {
        private ProductService productService;
        public HomeControl()
        {
            InitializeComponent();
            productService = new ProductService();
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<Product> ListProducts = productService.GetAll();
            ProductsItemsControl.ItemsSource=ListProducts;
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

        }



        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
