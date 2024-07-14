using BusinessObjects.Models;
using System.Collections.Generic;

namespace Services
{
    public interface IOrderDetailService
    {
        List<OrderDetail> GetAll();

        OrderDetail GetOrderDetailById(int id);

        void SaveOrderDetail(OrderDetail orderDetail);

        void UpdateOrderDetail(OrderDetail orderDetail);

        void DeleteOrderDetail(OrderDetail orderDetail);
    }
}
