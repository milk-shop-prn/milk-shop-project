using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null!;
        private static readonly object lockObject = new object();

        private OrderDetailDAO() { }

        public static OrderDetailDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public List<OrderDetail> GetAll()
        {
            using var db = new MilkShopContext();
            return db.OrderDetails.ToList();
        }

        public OrderDetail GetOrderDetailById(int id)
        {
            using var db = new MilkShopContext();
            return db.OrderDetails.FirstOrDefault(od => od.OrderDetailId.Equals(id));
        }

        public void SaveOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using var db = new MilkShopContext();
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Entry<OrderDetail>(orderDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using var db = new MilkShopContext();
                var od = db.OrderDetails.SingleOrDefault(od => od.OrderDetailId == orderDetail.OrderDetailId);
                if (od != null)
                {
                    db.OrderDetails.Remove(od);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

		public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
		{
			using var db = new MilkShopContext();
			return db.OrderDetails
                .Include(o => o.Product)
                .Where(o => o.OrderId == orderId)
                .ToList();
		}
	}
}
