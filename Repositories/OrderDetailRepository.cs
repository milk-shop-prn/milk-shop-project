using BusinessObjects.Models;
using DataAccessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void DeleteOrderDetail(OrderDetail orderDetail)
            => OrderDetailDAO.Instance.DeleteOrderDetail(orderDetail);

        public List<OrderDetail> GetAll()
            => OrderDetailDAO.Instance.GetAll();

        public OrderDetail GetOrderDetailById(int id)
            => OrderDetailDAO.Instance.GetOrderDetailById(id);

        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
            => OrderDetailDAO.Instance.GetOrderDetailsByOrderId(orderId);

		public void SaveOrderDetail(OrderDetail orderDetail)
            => OrderDetailDAO.Instance.SaveOrderDetail(orderDetail);

        public void UpdateOrderDetail(OrderDetail orderDetail)
            => OrderDetailDAO.Instance.UpdateOrderDetail(orderDetail);

    }
}
