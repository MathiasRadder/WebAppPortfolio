using System;
using System.Collections.Generic;

namespace DataPortfolio.Models;

public partial class ProjectType
{
    public int TypeId { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<ProjectPage> ProjectPages { get; set; } = new List<ProjectPage>();
}
