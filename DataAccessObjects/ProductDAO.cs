using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class ProductDAO
    {
        private static ProductDAO instance = null!;
        private static readonly object lockObject = new object();

        private ProductDAO() { }

        public static ProductDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Product> GetAll()
        {
            using var db = new MilkShopContext();
            return db.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            using var db = new MilkShopContext();
            return db.Products.FirstOrDefault(p => p.ProductId.Equals(id));
        }

        public void SaveProduct(Product product)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Products.Add(product);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Entry<Product>(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteProduct(Product product)
        {
            try
            {
                using var db = new MilkShopContext();
                var p = db.Products.SingleOrDefault(p => p.ProductId == product.ProductId);
                if (p != null)
                {
                    db.Products.Remove(p);
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
