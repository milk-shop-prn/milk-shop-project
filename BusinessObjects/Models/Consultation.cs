using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Consultation
{
    public int ConsultationId { get; set; }

    public int UserId { get; set; }

    public DateTime ConsultationDate { get; set; }

    public string? Status { get; set; }

    public virtual User User { get; set; } = null!;
}
