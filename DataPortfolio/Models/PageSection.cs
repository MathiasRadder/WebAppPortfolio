using System;
using System.Collections.Generic;

namespace DataPortfolio.Models;

public partial class PageSection
{
    public int SectionId { get; set; }

    public int PageId { get; set; }

    public int TypeId { get; set; }

    public string? Title { get; set; }

    public string? ImageName { get; set; }

    public string? ImageFileType { get; set; }

    public int SortOrder { get; set; }

    public virtual ProjectPage Page { get; set; } = null!;

    public virtual ICollection<TextBox> TextBoxes { get; set; } = new List<TextBox>();

    public virtual SectionType Type { get; set; } = null!;
}
