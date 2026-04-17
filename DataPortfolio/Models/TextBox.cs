using System;
using System.Collections.Generic;

namespace DataPortfolio.Models;

public partial class TextBox
{
    public int TextBoxId { get; set; }

    public int SectionId { get; set; }

    public int SortOrder { get; set; }

    public string? Title { get; set; }

    public virtual PageSection Section { get; set; } = null!;

    public virtual ICollection<TextPart> TextParts { get; set; } = new List<TextPart>();
}
