using DataPortfolio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using ServicesPortfolio.Services;
using System.Diagnostics;
using WebPortfolio.Models;
using WebPortfolio.Services;

namespace WebPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProjectPageService _projectPageService;
        private readonly SectionTypeService _sectionTypeService;
        private const string titleName = "projectTitle";
        private readonly IMemoryCache _cache;
        private readonly CreateProjectCardsVMService _createProjectCardsVMService;
        private readonly ProjectCardsRepositoryCache _projectCardsRepositoryCache;
        private readonly CreatePageSectionsVMService _createPageSectionsVMService;
        private readonly PageSectionsRepositoryCache _pageSectionsRepositoryCache;


        public HomeController(ILogger<HomeController> logger, IMemoryCache cache, 
            ProjectPageService projectService, SectionTypeService sectionTypeService, 
            CreateProjectCardsVMService createProjectCardsVMService, CreatePageSectionsVMService createPageSectionsVMService)
        {
            _logger = logger;
            _projectPageService = projectService;
            _sectionTypeService = sectionTypeService;

            _cache = cache;
            _createProjectCardsVMService = createProjectCardsVMService;
            _projectCardsRepositoryCache = new ProjectCardsRepositoryCache(_cache, _projectPageService, _createProjectCardsVMService);
            _createPageSectionsVMService = createPageSectionsVMService;
            _pageSectionsRepositoryCache = new PageSectionsRepositoryCache(_cache, _sectionTypeService, _projectPageService, _createPageSectionsVMService);
        }

        [Route("")]
        [Route("Home")]
        public async Task<IActionResult> Index()
        {
            List<ProjectCardViewModel>? projectCardList = await _projectCardsRepositoryCache.GetProjectCardList();
            if (projectCardList == null || projectCardList.Count == 0)
                return NotFound();
            return View(projectCardList);
        }

        [Route("Home/Project/{pageId::int}")]
        public async Task<IActionResult> ProjectPage(int pageId, string title, string imageFolder)
        {
            KeyValuePair<string, IReadOnlyList<PageSectionViewModel>?> pageSectionViewModel =
                await _pageSectionsRepositoryCache.GetProjectPagePairFromId(pageId);

            if (string.IsNullOrWhiteSpace(pageSectionViewModel.Key) || pageSectionViewModel.Value == null)
                return NotFound();

            ViewData[titleName] = pageSectionViewModel.Key;

            return View(pageSectionViewModel.Value);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
