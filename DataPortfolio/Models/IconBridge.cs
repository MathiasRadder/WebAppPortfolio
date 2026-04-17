using System;
using System.Collections.Generic;

namespace DataPortfolio.Models;

public partial class IconBridge
{
    public int IconBridgeId { get; set; }

    public int IconId { get; set; }

    public int PageId { get; set; }

    public int? IcondOrder { get; set; }

    public virtual Icon Icon { get; set; } = null!;

    public virtual ProjectPage Page { get; set; } = null!;
}
