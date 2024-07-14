using BusinessObjects.Models;
using System.Collections.Generic;

namespace Services
{
    public interface IOrderService
    {
        List<Order> GetAll();

        Order GetOrderById(int id);

        void SaveOrder(Order order);

        void UpdateOrder(Order order);

        void DeleteOrder(Order order);
    }
}
