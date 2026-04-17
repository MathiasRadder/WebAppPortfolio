using DataPortfolio.Models;
using WebPortfolio.Models;

namespace WebPortfolio.Services
{
    public class CreateProjectCardsVMService
    {
        public List<ProjectCardViewModel> CreateProjectCardsViewModels(IReadOnlyList<ProjectPage> projectPages)
        {
            List<ProjectCardViewModel> projectCardsviewModelList = new();
            foreach (var projectPage in projectPages)
            {
                if (projectPage == null)
                    continue;
                //Create model view
                ProjectPageViewModel tmpPPVM = new ProjectPageViewModel
                {
                    Title = projectPage.Title,
                    Id = projectPage.PageId,
                    Description = projectPage.ProjectDescription,
                    ImageFolder = projectPage.ImageFolder,
                    ImageLocation = HelperService.CreateImageLocation(projectPage.ImageFolder, projectPage.ImageName, null),
                    IconsVM = projectPage.IconBridges.ToList().ConvertAll(c =>
                    {
                        return new IconViewModel
                        {
                            ImageLocation = HelperService.CreateImageIconLocation(c.Icon.IconName, null),
                            IconName = c.Icon.IconName
                        };
                    })
                };

                //Add or make new 
                ProjectCardViewModel? psVM = projectCardsviewModelList
                    .FirstOrDefault(ps => ps.ProjectTypeTitle == projectPage.Type.Type);
                if (psVM == null || psVM == default)
                    projectCardsviewModelList.Add(new ProjectCardViewModel
                    {
                        IdProjectType = projectPage.Type.TypeId,
                        ProjectTypeTitle = projectPage.Type.Type,
                        ProjectPageList = new List<ProjectPageViewModel> { tmpPPVM }

                    });
                else
                    psVM.ProjectPageList.Add(tmpPPVM);
            }

            return projectCardsviewModelList;
        }
    }
}
