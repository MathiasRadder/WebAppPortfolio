using DataPortfolio.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using ServicesPortfolio.Services;
using WebPortfolio.Models;

namespace WebPortfolio.Services
{
    public class PageSectionsRepositoryCache
    {
        private readonly IMemoryCache _cache;
        private const string _cacheKeySectionList = "PageSectionList";
        private const string _cacheKeySectionTypeList = "SectionTypeList";
        private const string _cacheKeyPageIdList = "pageIdList";
        private readonly TimeSpan _cacheSlidingExpiration = TimeSpan.FromHours(2);
        private readonly TimeSpan _cacheAbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4);
        private readonly SectionTypeService _sectionTypeService;
        private readonly ProjectPageService _projectPageService;
        private readonly CreatePageSectionsVMService _createPageSectionsVMService;

        public PageSectionsRepositoryCache(IMemoryCache cache,
            SectionTypeService sectionTypeService,
            ProjectPageService projectPageService,
            CreatePageSectionsVMService createPageSectionsVMService)
        {
            _cache = cache;
            _sectionTypeService = sectionTypeService;
            _createPageSectionsVMService = createPageSectionsVMService;
            _projectPageService = projectPageService;
        }


        public async Task<KeyValuePair<string, IReadOnlyList<PageSectionViewModel>?>> GetProjectPagePairFromId(int id)
        {
            return await _cache.GetOrCreateAsync($"{_cacheKeySectionList}_{id}", async entry =>
            {
                ProjectPage? entireProjectPage = await _projectPageService.GetEntireProjectPageById(id);
                if (entireProjectPage == null || entireProjectPage.PageSections.IsNullOrEmpty())
                {
                    //Not found, will dispose the cache entry
                    entry.Dispose();
                    return new KeyValuePair<string, IReadOnlyList<PageSectionViewModel>?>(
                        string.Empty, null);
                }

                List<PageSectionViewModel>? pageSectionList = _createPageSectionsVMService
                .CreatePageSectionViewModels(
                    entireProjectPage.PageSections.ToList(),
                    await GetsectionTypeList(),
                    entireProjectPage.ImageFolder);
                if (pageSectionList.IsNullOrEmpty())
                {
                    //pageSectionList is null, will dispose the cache entry
                    entry.Dispose();
                    return new KeyValuePair<string, IReadOnlyList<PageSectionViewModel>?>(
                        string.Empty, null);
                }

                var projectPagePair = new KeyValuePair<string, IReadOnlyList<PageSectionViewModel>?>(
                    entireProjectPage.Title, pageSectionList);

                entry.SlidingExpiration = _cacheSlidingExpiration;
                entry.AbsoluteExpirationRelativeToNow = _cacheAbsoluteExpirationRelativeToNow;

                entry.Value = projectPagePair;
                return projectPagePair;
            });
        }

        private async Task<List<KeyValuePair<string, string>>?> GetsectionTypeList()
        {
            return await _cache.GetOrCreateAsync(_cacheKeySectionTypeList, async entry =>
            {
                List<KeyValuePair<string, string>>? sectionList = await _createPageSectionsVMService.CreateSectionTypeList(_sectionTypeService);
                if (sectionList.IsNullOrEmpty())
                {
                    entry.Dispose();
                    return null;
                }

                entry.SlidingExpiration = _cacheSlidingExpiration;
                entry.AbsoluteExpirationRelativeToNow = _cacheAbsoluteExpirationRelativeToNow;

                entry.Value = sectionList;
                return sectionList;
            });
        }

        //Not used anymore since its redundant, but interesting still
        public async Task<KeyValuePair<string, IReadOnlyList<PageSectionViewModel>?>> GetListCheckProjectPagePairFromId(int id)
        {
            //First check to server if that id is available
            IReadOnlyList<int>? pageIdList = await GetPageIdList();
            if (pageIdList.IsNullOrEmpty() || !pageIdList.Contains(id))
                return new KeyValuePair<string, IReadOnlyList<PageSectionViewModel>?>(
               string.Empty, null);

            return await GetProjectPagePairFromId(id);
        }

        private async Task<IReadOnlyList<int>?> GetPageIdList()
        {
            return await _cache.GetOrCreateAsync($"{_cacheKeyPageIdList}", async entry =>
            {
                IReadOnlyList<int> pageIdList = await _projectPageService.GetProjectPagesIdList();
                if (pageIdList.IsNullOrEmpty())
                {
                    entry.Dispose();
                    return null;
                }

                entry.SlidingExpiration = _cacheSlidingExpiration;
                entry.AbsoluteExpirationRelativeToNow = _cacheAbsoluteExpirationRelativeToNow;

                entry.Value = pageIdList;
                return pageIdList;
            });
        }
    }
}
