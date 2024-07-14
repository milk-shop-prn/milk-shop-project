using BusinessObjects.Models;
using DataAccessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public void DeleteOrder(Order order)
            => OrderDAO.Instance.DeleteOrder(order);

        public List<Order> GetAll()
            => OrderDAO.Instance.GetAll();

        public Order GetOrderById(int id)
            => OrderDAO.Instance.GetOrderById(id);

        public void SaveOrder(Order order)
            => OrderDAO.Instance.SaveOrder(order);

        public void UpdateOrder(Order order)
            => OrderDAO.Instance.UpdateOrder(order);
    }
}
