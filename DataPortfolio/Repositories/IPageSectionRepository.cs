using DataPortfolio.Models;

namespace DataPortfolio.Repositories
{
    public interface IPageSectionRepository
    {
        Task<IReadOnlyList<PageSection>> GetAllFromProjectId(int pageId);
        Task<IReadOnlyList<PageSection>> GetAllBaseFromProjectId(int pageId);
    }
}
