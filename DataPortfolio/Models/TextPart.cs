using System;
using System.Collections.Generic;

namespace DataPortfolio.Models;

public partial class TextPart
{
    public int TextBoxId { get; set; }

    public int SortOrder { get; set; }

    public string TextBody { get; set; } = null!;

    public int? IconId { get; set; }

    public int? IcondTextOrder { get; set; }

    public virtual Icon? Icon { get; set; }

    public virtual TextBox TextBox { get; set; } = null!;
}
