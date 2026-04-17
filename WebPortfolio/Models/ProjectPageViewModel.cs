using DataPortfolio.Models;

namespace WebPortfolio.Models
{
    public class ProjectPageViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageLocation { get; set; } = null!;
        public string? ImageName { get; set; } = null!;
        public string ImageFolder { get; set; } = null!;
        public List<IconViewModel> IconsVM { get; set; } = [];
    }
}
