using System;
using System.Collections.Generic;

namespace DataPortfolio.Models;

public partial class Icon
{
    public int IconId { get; set; }

    public string IconName { get; set; } = null!;

    public string? Link { get; set; }

    public string? Addtext { get; set; }

    public virtual ICollection<IconBridge> IconBridges { get; set; } = new List<IconBridge>();

    public virtual ICollection<TextPart> TextParts { get; set; } = new List<TextPart>();
}
