using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using ServicesPortfolio.Services;
using WebPortfolio.Models;

namespace WebPortfolio.Services
{
    public class ProjectCardsRepositoryCache
    {

        private readonly IMemoryCache _cache;
        private const string _cacheKey = "ProjectPagesList";
        private readonly TimeSpan _cacheSlidingExpiration = TimeSpan.FromHours(2);
        private readonly TimeSpan _cacheAbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4);
        private readonly ProjectPageService _projectService;
        private readonly CreateProjectCardsVMService _createProjectCardsVMService;

        public ProjectCardsRepositoryCache(IMemoryCache cache, ProjectPageService projectService, CreateProjectCardsVMService createProjectCardsVMService)
        {
            _projectService = projectService;
            _cache = cache;
            _createProjectCardsVMService = createProjectCardsVMService;
        }

        public async Task<List<ProjectCardViewModel>?> GetProjectCardList()
        {
            return await _cache.GetOrCreateAsync(_cacheKey, async entry =>
            {
                var ProjectCardsList = _createProjectCardsVMService.CreateProjectCardsViewModels(await _projectService.GetProjectPagesList());

                if (ProjectCardsList.IsNullOrEmpty())
                {
                    entry.Dispose();
                    return null;
                }

                entry.SlidingExpiration = _cacheSlidingExpiration;
                entry.AbsoluteExpirationRelativeToNow = _cacheAbsoluteExpirationRelativeToNow;

                entry.Value = ProjectCardsList;
                return ProjectCardsList;
            });
        }
    }
}
