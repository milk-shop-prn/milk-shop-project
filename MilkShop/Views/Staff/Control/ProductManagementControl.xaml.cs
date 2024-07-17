using BusinessObjects.Models;
using MilkShop.config;
using Repositories;
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

namespace MilkShop.Views.Staff.Control
{
    /// <summary>
    /// Interaction logic for ProductManagementControl.xaml
    /// </summary>
    public partial class ProductManagementControl : UserControl
    {
        IProductRepository _repo = new ProductRepository();
        bool NewOrUpdate = false;
        public ProductManagementControl()
        {
            InitializeComponent();
        }
        private void ProductControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProduct();
        }
        private void LoadProduct()
        {
            var listProduct = _repo.GetAll();
            dbProduct.ItemsSource = listProduct;
        }
        private void DbProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dbProduct.SelectedItem is Product selectedProduct)
            {
                txtProductId.Text = selectedProduct.ProductId.ToString();
                txtProductName.Text = selectedProduct.ProductName;
                txtDescription.Text = selectedProduct.Description;
                txtPrice.Text = selectedProduct.Price.ToString();
                txtStock.Text = selectedProduct.Stock.ToString();
                txtCategory.Text = selectedProduct.Category;
                txtImageUrl.Text = selectedProduct.ImageUrl;
            }
            else
            {
                ClearTextbox();
            }
        }
        private void ClearTextbox()
        {
            txtProductId.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtCategory.Text = string.Empty;
            txtImageUrl.Text = string.Empty;
        }

        private void EnableText(bool status)
        {
            txtProductName.IsEnabled = status;
            txtDescription.IsEnabled = status;
            txtPrice.IsEnabled = status;
            txtStock.IsEnabled = status;
            txtCategory.IsEnabled = status;
            txtImageUrl.IsEnabled = status;
        }
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            if (btnNew.Content.ToString() == "New")
            {
                btnNew.Content = "Cancel";
                btnSave.IsEnabled = true;
                btnUpdate.IsEnabled = false;
                NewOrUpdate = true;

                ClearTextbox();
                EnableText(true);
            }
            else if (btnNew.Content.ToString() == "Cancel")
            {
                btnNew.Content = "New";
                btnSave.IsEnabled = false;
                btnUpdate.IsEnabled = true;
                NewOrUpdate = false;

                EnableText(false);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dbProduct.SelectedItem is Product selectedProduct)
            {
                if (btnUpdate.Content.ToString() == "Update")
                {
                    EnableText(true);
                    txtProductId.IsEnabled = false;

                    btnUpdate.Content = "Cancel";
                    btnSave.IsEnabled = true;
                    NewOrUpdate = false;
                }
                else
                {
                    EnableText(false);
                    btnUpdate.Content = "Update";
                    btnSave.IsEnabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please select a product to update", "Product Management System",
                            MessageBoxButton.OK
                           , MessageBoxImage.Information);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtProductName.Text.Trim() == "" || txtDescription.Text.Trim() == "" ||
                txtPrice.Text.Trim() == "" || txtStock.Text.Trim() == "" ||
                txtCategory.Text.Trim() == "" || txtImageUrl.Text.Trim() == "")
            {
                MessageBox.Show("All fields required."
                           , "Product Management System", MessageBoxButton.OK
                           , MessageBoxImage.Information);
            }
            else
            {
                var product = new Product
                {
                    ProductName = txtProductName.Text,
                    Description = txtDescription.Text,
                    Price = decimal.Parse(txtPrice.Text),
                    Stock = int.Parse(txtStock.Text),
                    Category = txtCategory.Text,
                    ImageUrl = txtImageUrl.Text
                };

                if (NewOrUpdate)
                {
                    _repo.SaveProduct(product);
                    MessageBox.Show("New product added."
                           , "Success", MessageBoxButton.OK
                           , MessageBoxImage.Information);
                    btnNew.Content = "New";
                }
                else
                {
                    product.ProductId = int.Parse(txtProductId.Text);
                    product.Deleted = false;
                    _repo.UpdateProduct(product);
                    MessageBox.Show("Product updated."
                           , "Success", MessageBoxButton.OK
                           , MessageBoxImage.Information);
                    btnUpdate.Content = "Update";
                }

                EnableText(false);
                btnSave.IsEnabled = false;
                LoadProduct();
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dbProduct.SelectedItem is Product selectedProduct)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this product?",
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    selectedProduct.Deleted = true;
                    _repo.UpdateProduct(selectedProduct);
                    LoadProduct();
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete."
                       , "Warning", MessageBoxButton.OK
                       , MessageBoxImage.Warning);
            }
        }
    }
}
