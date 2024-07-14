using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Point
{
    public int PointId { get; set; }

    public int UserId { get; set; }

    public int Points { get; set; }

    public DateTime DateEarned { get; set; }

    public virtual User User { get; set; } = null!;
}
