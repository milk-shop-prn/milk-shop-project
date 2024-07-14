using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPointService
    {
        Point GetPointById(int pointId);

        List<Point> GetAllPoints();

        void SavePoint(Point point);

        void UpdatePoint(Point point);

        void DeletePoint(Point point);
    }
}
