namespace WebPortfolio.Models
{
    public class PageSectionViewModel
    {
        public string? Title { get; set; }
        public string? ImageLocation { get; set; }
        public List<TextBoxViewModel> TextBoxViewModelList { get; set; } = new List<TextBoxViewModel>();
        public List<TextBoxViewModel> otherTextBoxViewModelList { get; set; } = new List<TextBoxViewModel>();
        public string ViewName { get; set; } = null!;
    }


}
