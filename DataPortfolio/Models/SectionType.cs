using System;
using System.Collections.Generic;

namespace DataPortfolio.Models;

public partial class SectionType
{
    public int TypeId { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<PageSection> PageSections { get; set; } = new List<PageSection>();
}
