using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class PointDAO
    {
        private static PointDAO instance = null!;
        private static readonly object lockObject = new object();

        public PointDAO() { }

        public static PointDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new PointDAO();
                    }
                    return instance;
                }
            }
        }

        public Point GetPointById(int pointId)
        {
            using var db = new MilkShopContext();
            return db.Points.SingleOrDefault(p => p.PointId == pointId);
        }

        public List<Point> GetAllPoints()
        {
            using var db = new MilkShopContext();
            return db.Points.ToList();
        }

        public void SavePoint(Point point)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Points.Add(point);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdatePoint(Point point)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Entry(point).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeletePoint(Point point)
        {
            try
            {
                using var db = new MilkShopContext();
                var existingPoint = db.Points.Find(point.PointId);
                if (existingPoint != null)
                {
                    db.Points.Remove(existingPoint);
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
