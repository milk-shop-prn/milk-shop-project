﻿using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IReportService
    {
        Report GetReportById(int reportId);

        List<Report> GetAllReports();

        void SaveReport(Report report);

        void UpdateReport(Report report);

        void DeleteReport(Report report);
    }
}