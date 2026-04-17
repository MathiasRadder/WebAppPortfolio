using System;
using System.Collections.Generic;

namespace DataPortfolio.Models;

public partial class ProjectPage
{
    public int PageId { get; set; }

    public int TypeId { get; set; }

    public string Title { get; set; } = null!;

    public string? ImageName { get; set; }

    public string? ProjectDescription { get; set; }

    public string ImageFolder { get; set; } = null!;

    public virtual ICollection<IconBridge> IconBridges { get; set; } = new List<IconBridge>();

    public virtual ICollection<PageSection> PageSections { get; set; } = new List<PageSection>();

    public virtual ProjectType Type { get; set; } = null!;
}
