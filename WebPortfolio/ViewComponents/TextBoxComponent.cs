using Microsoft.AspNetCore.Mvc;
using WebPortfolio.Models;

namespace WebPortfolio.ViewComponents
{
    public class TextBoxComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<TextBoxViewModel> textBoxVMList)
        {
            return View("Default", textBoxVMList);
        }
    }
}
