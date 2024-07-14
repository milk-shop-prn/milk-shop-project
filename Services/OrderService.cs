using BusinessObjects.Models;
using Repositories;
using System.Collections.Generic;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService()
        {
            this.orderRepository = new OrderRepository();
        }

        public void DeleteOrder(Order order)
        {
            this.orderRepository.DeleteOrder(order);
        }

        public List<Order> GetAll()
        {
            return this.orderRepository.GetAll();
        }

        public Order GetOrderById(int id)
        {
            return this.orderRepository.GetOrderById(id);
        }

        public void SaveOrder(Order order)
        {
            this.orderRepository.SaveOrder(order);
        }

        public void UpdateOrder(Order order)
        {
            this.orderRepository.UpdateOrder(order);
        }
    }
}
