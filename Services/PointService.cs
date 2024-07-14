using BusinessObjects.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PointService : IPointService
    {
        private readonly IPointRepository pointRepository;

        public PointService(IPointRepository pointRepository)
        {
            this.pointRepository = pointRepository;
        }

        public void DeletePoint(Point point)
            => pointRepository.DeletePoint(point);

        public List<Point> GetAllPoints()
            => pointRepository.GetAllPoints();

        public Point GetPointById(int pointId)
            => pointRepository.GetPointById(pointId);

        public void SavePoint(Point point)
            => pointRepository.SavePoint(point);

        public void UpdatePoint(Point point)
            => pointRepository.UpdatePoint(point);
    }
}
