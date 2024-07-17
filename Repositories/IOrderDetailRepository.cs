using BusinessObjects.Models;
using System.Collections.Generic;

namespace Repositories
{
    public interface IOrderDetailRepository
    {
        List<OrderDetail> GetAll();

        OrderDetail GetOrderDetailById(int id);

        void SaveOrderDetail(OrderDetail orderDetail);

        void UpdateOrderDetail(OrderDetail orderDetail);

        void DeleteOrderDetail(OrderDetail orderDetail);

        List<OrderDetail> GetOrderDetailsByOrderId(int orderId);

	}
}
