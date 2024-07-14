using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public string? ReportType { get; set; }

    public string? Description { get; set; }

    public DateTime ReportDate { get; set; }

    public int? ReportedBy { get; set; }

    public string? Status { get; set; }

    public virtual User? ReportedByNavigation { get; set; }
}
