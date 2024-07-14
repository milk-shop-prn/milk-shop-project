using BusinessObjects.Models;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ReportRepository : IReportRepository
    {
        public void DeleteReport(Report report)
            => ReportDAO.Instance.DeleteReport(report);

        public List<Report> GetAllReports()
            => ReportDAO.Instance.GetAllReports();

        public Report GetReportById(int reportId)
            => ReportDAO.Instance.GetReportById(reportId);

        public void SaveReport(Report report)
            => ReportDAO.Instance.SaveReport(report);

        public void UpdateReport(Report report)
            => ReportDAO.Instance.UpdateReport(report);
    }
}
