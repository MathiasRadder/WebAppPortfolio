using DataPortfolio.Models;

namespace WebPortfolio.Models
{
    public class TextBoxViewModel
    {
        public string? Title { get; set; }
        public List<TextPartViewModel> textPartList { get; set; } = new List<TextPartViewModel>();
    }
}
