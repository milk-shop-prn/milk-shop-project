using BusinessObjects.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        public void DeleteReport(Report report)
            => reportRepository.DeleteReport(report);

        public List<Report> GetAllReports()
            => reportRepository.GetAllReports();

        public Report GetReportById(int reportId)
            => reportRepository.GetReportById(reportId);

        public void SaveReport(Report report)
            => reportRepository.SaveReport(report);

        public void UpdateReport(Report report)
            => reportRepository.UpdateReport(report);
    }
}
