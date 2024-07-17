using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Article
{
    public int ArticleId { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public int AuthorId { get; set; }

    public DateTime PublishDate { get; set; }

    public bool? Deleted { get; set; }

    public virtual User Author { get; set; } = null!;
}
