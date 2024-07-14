using BusinessObjects.Models;
using System.Collections.Generic;

namespace Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetAll();

        Order GetOrderById(int id);

        void SaveOrder(Order order);

        void UpdateOrder(Order order);

        void DeleteOrder(Order order);
    }
}
