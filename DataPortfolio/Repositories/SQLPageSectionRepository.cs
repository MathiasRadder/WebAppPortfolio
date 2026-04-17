using DataPortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace DataPortfolio.Repositories
{
    public class SQLPageSectionRepository : IPageSectionRepository
    {
        private readonly ProjectsWebContext context;
        public SQLPageSectionRepository(ProjectsWebContext context)
        {
            this.context = context;
        }
        async Task<IReadOnlyList<PageSection>> IPageSectionRepository.GetAllBaseFromProjectId(int pageId)
        {
            return await context.PageSection.Include(c => c.Type)
                .Where(c => c.PageId == pageId).OrderBy(c => c.SortOrder).AsNoTracking().ToListAsync();
        }

        async Task<IReadOnlyList<PageSection>> IPageSectionRepository.GetAllFromProjectId(int pageId)
        {
            return await context.PageSection.Include(c => c.Type).Where(c => c.PageId == pageId).OrderBy(c => c.SortOrder)
                .Include(c => c.TextBoxes.OrderBy(c => c.SortOrder))
                .ThenInclude(tb => tb.TextParts.OrderBy(c => c.SortOrder))
                .ThenInclude(tb => tb.Icon).AsNoTracking().ToListAsync(); ;
        }
    }
}
