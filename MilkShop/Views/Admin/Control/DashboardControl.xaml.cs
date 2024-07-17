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

namespace MilkShop.Views.Admin.Control
{
    /// <summary>
    /// Interaction logic for DashboardControl.xaml
    /// </summary>
    public partial class DashboardControl : UserControl
    {
        IOrderService orderService;
        IUserService userService;
        IProductService productService;
        public DashboardControl()
        {
            InitializeComponent();
            orderService = new OrderService();
            userService = new UserService();
            productService = new ProductService();
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            var totalOrder = orderService.GetAll().Count(order => order.OrderStatus.Equals("Delivered"));
            var totalReveue = orderService.GetAll().Where(order => order.OrderStatus.Equals("Delivered")).Sum(order => order.TotalPrice);
            var totalUser = userService.GetAll().Count(user => user.Deleted != true);
            var totalProduct = productService.GetAll().Count(product => product.Deleted != true);

            BlkOrder.Text = totalOrder.ToString();
            BlkRevenue.Text = Math.Round(totalReveue).ToString();
            BlkProduct.Text = totalProduct.ToString();
            BlkUser.Text = totalUser.ToString();
        }
    }
}
