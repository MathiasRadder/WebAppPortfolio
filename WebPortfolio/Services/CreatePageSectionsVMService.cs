using DataPortfolio.Models;
using ServicesPortfolio.Services;
using WebPortfolio.Models;

namespace WebPortfolio.Services
{
    public class CreatePageSectionsVMService
    {
        private const string videoMiddle = "Video middle";

        public async Task<List<KeyValuePair<string, string>>> CreateSectionTypeList(SectionTypeService sectionTypeService)
        {
            IReadOnlyList<SectionType> sectionTypelist = await sectionTypeService.GetSectionTypeList();
            List<KeyValuePair<string, string>> sectionTypeList = [];
            foreach (SectionType sectionType in sectionTypelist)
            {
                string fileName = sectionType.Type.Replace(" ", "_");
                sectionTypeList.Add(new KeyValuePair<string, string>(sectionType.Type, fileName));
            }
            return sectionTypeList;
        }

        public List<PageSectionViewModel>? CreatePageSectionViewModels(IReadOnlyList<PageSection> pageSections, List<KeyValuePair<string, string>>? sectionTypeList, string imageFolder)
        {
            //Here we get de cache or repository data of the sectiontype list
            if (sectionTypeList == null)
                return null;
            List<PageSectionViewModel> pageSectionVMList = new();
            foreach (PageSection pageSection in pageSections)
            {
                List<TextBoxViewModel> tmpTextBoxVMList = new();
                foreach (TextBox textBox in pageSection.TextBoxes)
                {
                    tmpTextBoxVMList.Add(new TextBoxViewModel
                    {
                        Title = textBox.Title,
                        textPartList = textBox.TextParts.ToList().ConvertAll(tp =>
                        {
                            IconViewModel? iconVM = null;
                            if (tp.Icon != null)
                            {
                                iconVM = new();
                                iconVM.ImageLocation = HelperService.CreateImageIconLocation(tp.Icon.IconName, null);
                                iconVM.IconName = tp.Icon.IconName;
                                iconVM.Addtext = tp.Icon.Addtext;
                                iconVM.Link = tp.Icon.Link;
                            }

                            return new TextPartViewModel
                            {
                                Text = tp.TextBody,
                                IcondTextOrder = tp.IcondTextOrder,
                                IconVM = iconVM,
                            };
                        })
                    });
                }

                pageSectionVMList.Add(new PageSectionViewModel
                {
                    Title = pageSection.Title,
                    ImageLocation = pageSection.Type.Type != videoMiddle ? HelperService.CreateImageLocation(imageFolder, pageSection.ImageName, pageSection.ImageFileType) : pageSection.ImageName,
                    TextBoxViewModelList = tmpTextBoxVMList,
                    ViewName = CreateVCViewMethod(sectionTypeList, pageSection.Type.Type)

                });
            }
            return pageSectionVMList;
        }

        private string CreateVCViewMethod(List<KeyValuePair<string, string>> sectionTypeList, string typeName)
        {
            //Check which type it is, default one is goes to the first one
            KeyValuePair<string, string> sectionType = sectionTypeList.FirstOrDefault(st => st.Key == typeName);
            if (string.IsNullOrWhiteSpace(sectionType.Key))
                return sectionTypeList.First().Value;
            return sectionType.Value;
        }
    }
}
