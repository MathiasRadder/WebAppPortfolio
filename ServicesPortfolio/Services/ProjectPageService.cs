using DataPortfolio.Repositories;
using DataPortfolio.Models;
using System.Threading.Tasks;
namespace ServicesPortfolio.Services
{
    public class ProjectPageService
    {
        private readonly IProjectPageRepository projectPageRepository;

        public ProjectPageService(IProjectPageRepository projectRepository)
        {
            this.projectPageRepository = projectRepository;
        }

        public async Task<IReadOnlyList<ProjectPage>> GetProjectPagesList()
        {
            return await projectPageRepository.GetList();
        }

        public async Task<ProjectPage?> GetEntireProjectPageById(int pageId)
        {
            return await projectPageRepository.GetEntireProjectPageById(pageId);
        }

        public async Task<IReadOnlyList<int>> GetProjectPagesIdList()
        {
            return await projectPageRepository.GetPageIdList();
        }

        public async Task AddStuffTest()
        {
            await projectPageRepository.AddDataToDatabase();
        }
    }
}
