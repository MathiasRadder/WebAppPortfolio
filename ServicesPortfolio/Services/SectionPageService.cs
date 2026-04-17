using DataPortfolio.Models;
using DataPortfolio.Repositories;

namespace ServicesPortfolio.Services
{
    public class SectionPageService
    {
        private readonly IPageSectionRepository projectRepository;

        public SectionPageService(IPageSectionRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }


        public async Task<IReadOnlyList<PageSection>> GetlistProjectPages(int pageId)
        {
            return await projectRepository.GetAllFromProjectId(pageId);
        }
    }
}
