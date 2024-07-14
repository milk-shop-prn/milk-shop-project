using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Voucher
{
    public int VoucherId { get; set; }

    public string Code { get; set; } = null!;

    public decimal? Discount { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public bool? IsUsed { get; set; }
}
