using DataPortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace DataPortfolio.Repositories
{
    public class SQLSectionTypeRepository : ISectionTypeRepository
    {
        private readonly ProjectsWebContext context;
        public SQLSectionTypeRepository(ProjectsWebContext context)
        {
            this.context = context;
        }
        public async Task<IReadOnlyList<SectionType>> GetList()
        {
            return await context.SectionType.AsNoTracking().ToListAsync();
        }
    }
}
