using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class ReportDAO
    {
        private static ReportDAO instance = null!;
        private static readonly object lockObject = new object();

        private ReportDAO() { }

        public static ReportDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new ReportDAO();
                    }
                    return instance;
                }
            }
        }

        public Report GetReportById(int reportId)
        {
            using var db = new MilkShopContext();
            return db.Reports.SingleOrDefault(r => r.ReportId == reportId);
        }

        public List<Report> GetAllReports()
        {
            using var db = new MilkShopContext();
            return db.Reports.ToList();
        }

        public void SaveReport(Report report)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Reports.Add(report);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateReport(Report report)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Entry(report).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteReport(Report report)
        {
            try
            {
                using var db = new MilkShopContext();
                var existingReport = db.Reports.Find(report.ReportId);
                if (existingReport != null)
                {
                    db.Reports.Remove(existingReport);
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
