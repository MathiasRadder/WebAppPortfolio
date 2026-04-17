using Microsoft.AspNetCore.Mvc;
using WebPortfolio.Models;

namespace WebPortfolio.ViewComponents
{
    public class LinkTextBoxComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<TextBoxViewModel> textBoxVMList)
        {
            return View("Default", textBoxVMList);
        }
    }
}
