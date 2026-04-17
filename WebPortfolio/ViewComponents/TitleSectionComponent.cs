using Microsoft.AspNetCore.Mvc;
using WebPortfolio.Models;

namespace WebPortfolio.ViewComponents
{
    public class TitleSectionComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string? sectionTitle)
        {
            return View("Default", sectionTitle);
        }
    }
}
