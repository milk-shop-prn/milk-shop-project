using BusinessObjects.Models;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PointRepository : IPointRepository
    {
        public void DeletePoint(Point point)
            => PointDAO.Instance.DeletePoint(point);

        public List<Point> GetAllPoints()
            => PointDAO.Instance.GetAllPoints();

        public Point GetPointById(int pointId)
            => PointDAO.Instance.GetPointById(pointId);

        public void SavePoint(Point point)
            => PointDAO.Instance.SavePoint(point);

        public void UpdatePoint(Point point)
            => PointDAO.Instance.UpdatePoint(point);
    }
}
