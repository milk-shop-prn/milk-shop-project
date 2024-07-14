using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class OrderDAO
    {
        private static OrderDAO instance = null!;
        private static readonly object lockObject = new object();

        private OrderDAO() { }

        public static OrderDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Order> GetAll()
        {
            using var db = new MilkShopContext();
            return db.Orders.ToList();
        }

        public Order GetOrderById(int id)
        {
            using var db = new MilkShopContext();
            return db.Orders.FirstOrDefault(o => o.OrderId.Equals(id));
        }

        public void SaveOrder(Order order)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Orders.Add(order);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Entry<Order>(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOrder(Order order)
        {
            try
            {
                using var db = new MilkShopContext();
                var o = db.Orders.SingleOrDefault(o => o.OrderId == order.OrderId);
                if (o != null)
                {
                    db.Orders.Remove(o);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
