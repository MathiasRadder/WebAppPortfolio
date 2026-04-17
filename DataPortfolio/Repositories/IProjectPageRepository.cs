using DataPortfolio.Models;

namespace DataPortfolio.Repositories
{
    public interface IProjectPageRepository
    {
        Task<IReadOnlyList<ProjectPage>> GetList();
        Task<ProjectPage?> GetEntireProjectPageById(int pageId);
        Task<IReadOnlyList<int>> GetPageIdList();

        Task AddDataToDatabase();
    }
}
