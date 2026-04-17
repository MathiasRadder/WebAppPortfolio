using DataPortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace DataPortfolio.Repositories
{
    public class SQLProjectPageRepository : IProjectPageRepository
    {
        private readonly ProjectsWebContext context;
        public SQLProjectPageRepository(ProjectsWebContext context)
        {
            this.context = context;
        }
        public async Task<IReadOnlyList<ProjectPage>> GetList()
        {
            return await context.ProjectPage.Include(c => c.Type)
                .Include(c => c.IconBridges.OrderBy(ib => ib.IcondOrder))
                .ThenInclude(c => c.Icon).OrderBy(c => c.TypeId)
                .AsNoTracking().ToListAsync();

        }

        public async Task<ProjectPage?> GetEntireProjectPageById(int pageId)
        {
            return await context.ProjectPage.Where(p => p.PageId == pageId)
                .Include(p => p.PageSections)
                .ThenInclude(ps => ps.Type)
                .Include(p => p.PageSections.OrderBy(ps => ps.SortOrder))
                .ThenInclude(ps => ps.TextBoxes.OrderBy(tb => tb.SortOrder))
                .ThenInclude(tb => tb.TextParts.OrderBy(c => c.SortOrder))
                .ThenInclude(tp => tp.Icon)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }


        public async Task<IReadOnlyList<int>> GetPageIdList()
        {
            return await context.ProjectPage.Select(p => p.PageId).ToListAsync();
        }

        public async Task AddDataToDatabase()
        {
            throw new NotImplementedException();
        }

    }
}


