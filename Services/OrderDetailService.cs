using BusinessObjects.Models;
using Repositories;
using System.Collections.Generic;

namespace Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository orderDetailRepository;
        public OrderDetailService()
        {
            this.orderDetailRepository = new OrderDetailRepository();
        }

        public void DeleteOrderDetail(OrderDetail orderDetail)
        {
            this.orderDetailRepository.DeleteOrderDetail(orderDetail);
        }

        public List<OrderDetail> GetAll()
        {
            return this.orderDetailRepository.GetAll();
        }

        public OrderDetail GetOrderDetailById(int id)
        {
            return this.orderDetailRepository.GetOrderDetailById(id);
        }

        public void SaveOrderDetail(OrderDetail orderDetail)
        {
            this.orderDetailRepository.SaveOrderDetail(orderDetail);
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            this.orderDetailRepository.UpdateOrderDetail(orderDetail);
        }
    }
}
