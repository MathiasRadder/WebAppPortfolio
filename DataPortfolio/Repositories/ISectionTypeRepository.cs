using DataPortfolio.Models;

namespace DataPortfolio.Repositories
{
    public interface ISectionTypeRepository
    {
        Task<IReadOnlyList<SectionType>> GetList();
    }
}
