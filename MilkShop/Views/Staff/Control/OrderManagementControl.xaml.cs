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
    /// Interaction logic for OrderManagementControl.xaml
    /// </summary>
    public partial class OrderManagementControl : UserControl
    {
        IOrderRepository _order = new OrderRepository();
        IOrderDetailRepository _detail = new OrderDetailRepository();
        IProductRepository _product = new ProductRepository();
        IUserRepository _user = new UserRepository();

        bool NewOrUpdate = false;

        public OrderManagementControl()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadOrder();
            comboxBoxProduct();
            comboxBoxUser();
        }

        private void LoadOrder()
        {
            var list = _order.GetAll();
            dbOrder.ItemsSource = list;
        }

        private void LoadOrderDetails(int orderId)
        {
            var list = _detail.GetOrderDetailsByOrderId(orderId);
            dbOrderDetails.ItemsSource = list;
        }

        private void comboxBoxUser()
        {
            try
            {
                var list = _user.GetAll().Where(user => user.Deleted == false);
                listName.ItemsSource = list;
                listName.DisplayMemberPath = "FullName";
                listName.SelectedValuePath = "UserId";

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void comboxBoxProduct()
        {
            try
            {
                var list = _product.GetAll().Where(product => product.Deleted == false);
                listProduct.ItemsSource = list;
                listProduct.DisplayMemberPath = "ProductName";
                listProduct.SelectedValuePath = "ProductId";

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void dbOrderDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void dbOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dbOrder.SelectedItem is Order selectedItem)
            {
                var listDetails = _detail.GetOrderDetailsByOrderId(selectedItem.OrderId);
                dbOrderDetails.ItemsSource = listDetails;

                txtOrderID.Text = selectedItem.OrderId.ToString();
                listName.SelectedValue = selectedItem.User.UserId;
                orderDate.Text = selectedItem.OrderDate.ToString();
                txtTotalPrice.Text = selectedItem.TotalPrice.ToString();
                txtStatus.Text = selectedItem.OrderStatus;
            }
            else
            {
                txtOrderID.Text = string.Empty;
                listName.SelectedIndex = -1;
                orderDate.Text = string.Empty;
                txtTotalPrice.Text = string.Empty;
                txtStatus.Text = string.Empty;
            }
        }

        private void EnableOrderBox(bool status)
        {
            listName.IsEnabled = status;
            orderDate.IsEnabled = status;
            txtStatus.IsEnabled = status;
        }

        private void ClearOrderBox()
        {
            txtOrderID.Text = string.Empty;
            listName.SelectedIndex = -1;
            orderDate.Text = string.Empty;
            txtTotalPrice.Text = string.Empty;
            txtStatus.Text = string.Empty;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            if (btnNew.Content.ToString() == "New order")
            {
                btnNew.Content = "Cancel";
                btnSave.IsEnabled = true;
                btnUpdate.IsEnabled = false;
                NewOrUpdate = true;

                ClearOrderBox();
                EnableOrderBox(true);
            }
            else if (btnNew.Content.ToString() == "Cancel")
            {
                btnNew.Content = "New order";
                btnSave.IsEnabled = false;
                btnUpdate.IsEnabled = true;
                NewOrUpdate = false;

                EnableOrderBox(false);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dbOrder.SelectedItem is Order selectedUser)
            {
                if (btnUpdate.Content.ToString() == "Update order")
                {
                    EnableOrderBox(true);

                    btnUpdate.Content = "Cancel";
                    btnSave.IsEnabled = true;
                    NewOrUpdate = false;
                }
                else
                {
                    EnableOrderBox(false);
                    btnUpdate.Content = "Update order";
                    btnSave.IsEnabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please select a order to update", "Order Management System",
                            MessageBoxButton.OK
                           , MessageBoxImage.Information);
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (listName.SelectedIndex == -1 || orderDate.Text == string.Empty ||
                txtStatus.Text.Trim() == "")
            {
                MessageBox.Show("All fields required."
                           , "Order Management System", MessageBoxButton.OK
                           , MessageBoxImage.Information);
            }
            else
            {
                if (NewOrUpdate)
                {
                    var order = new Order
                    {
                        UserId = (int)listName.SelectedValue,
                        OrderDate = orderDate.SelectedDate.Value,
                        TotalPrice = 0,
                        OrderStatus = txtStatus.Text,
                    };

                    _order.SaveOrder(order);
                    MessageBox.Show("New order added."
                           , "Success", MessageBoxButton.OK
                           , MessageBoxImage.Information);
                    btnNew.Content = "New order";
                    btnUpdate.IsEnabled = true;
                }
                else
                {
                    var order = new Order
                    {
                        OrderId = int.Parse(txtOrderID.Text),
                        UserId = listName.SelectedIndex,
                        OrderDate = orderDate.SelectedDate.Value,
                        OrderStatus = txtStatus.Text,
                    };

                    _order.UpdateOrder(order);
                    btnUpdate.Content = "Update order";
                }

                EnableOrderBox(false);
                btnSave.IsEnabled = false;
                LoadOrder();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dbOrder.SelectedItem is Order selected)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this order?",
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    selected.OrderStatus = "Canceled";
                    _order.UpdateOrder(selected);
                    LoadOrder();
                }

            }
            else
            {
                MessageBox.Show("Please select an order to delete."
                       , "Warning", MessageBoxButton.OK
                       , MessageBoxImage.Warning);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (dbOrder.SelectedItem is Order selectedItem
                && selectedItem.OrderStatus != "Confirmed")
            {
                if (listProduct.SelectedItem is Product selectedProduct)
                {
                    var product = _product.GetProductById(selectedProduct.ProductId);
                    var detail = new OrderDetail
                    {
                        OrderId = selectedItem.OrderId,
                        ProductId = selectedProduct.ProductId,
                        Quantity = int.Parse(txtQuantity.Text),
                        UnitPrice = product.Price,
                    };
                    _detail.SaveOrderDetail(detail);

                    selectedItem.TotalPrice += detail.Quantity * detail.UnitPrice;
                    _order.UpdateOrder(selectedItem);

                    LoadOrder();
                    LoadOrderDetails(selectedItem.OrderId);
                }
                else
                {
                    MessageBox.Show("Please select a product."
                           , "Notification", MessageBoxButton.OK
                           , MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select an order."
                       , "Notification", MessageBoxButton.OK
                       , MessageBoxImage.Information);
            }
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dbOrder.SelectedItem is Order selectedOrder && dbOrderDetails.SelectedItem is OrderDetail selectedDetail)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this order detail?",
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _detail.DeleteOrderDetail(selectedDetail);

                    selectedOrder.TotalPrice = _detail.GetOrderDetailsByOrderId(selectedOrder.OrderId)
                        .Sum(d => d.Quantity * d.UnitPrice);
                    _order.UpdateOrder(selectedOrder);

                    LoadOrderDetails(selectedOrder.OrderId);
                    LoadOrder();
                }
            }
            else
            {
                MessageBox.Show("Please select an order detail to delete."
                       , "Warning", MessageBoxButton.OK
                       , MessageBoxImage.Warning);
            }
        }
    }
}
