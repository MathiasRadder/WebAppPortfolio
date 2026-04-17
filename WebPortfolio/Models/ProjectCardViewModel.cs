namespace WebPortfolio.Models
{
    public class ProjectCardViewModel
    {
        public int IdProjectType { get; set; }
        public string ProjectTypeTitle { get; set; } = null!;
        public List<ProjectPageViewModel> ProjectPageList { get; set; } = new List<ProjectPageViewModel>();

    }
}
