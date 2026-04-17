using Microsoft.AspNetCore.Mvc;
using WebPortfolio.Models;

namespace WebPortfolio.ViewComponents
{
    public class ViewSectionComponent : ViewComponent
    {
        private const string doubleText = "Double_text";
        public IViewComponentResult Invoke(PageSectionViewModel pageSectionViewModel)
        {
            if (pageSectionViewModel.ViewName == doubleText)
                SplitTextboxesInTwo(pageSectionViewModel);
            return View(pageSectionViewModel.ViewName, pageSectionViewModel);
        }


        private void SplitTextboxesInTwo(PageSectionViewModel pageSectionViewModel)
        {
            //first we check if we dont already have the other otherTextBoxViewModelList filled in
            if (pageSectionViewModel.otherTextBoxViewModelList != null && pageSectionViewModel.otherTextBoxViewModelList.Count != 0)
                return;

            //here we edit the PageSectionViewModel for the correct version of the section type
            //calculate the size
            List<int> textBoxSize = new List<int>();
            for (int i = 0; i < pageSectionViewModel.TextBoxViewModelList.Count; i++)
            {
                textBoxSize.Add(0);
                foreach (var textPart in pageSectionViewModel.TextBoxViewModelList[i].textPartList)
                    textBoxSize[textBoxSize.Count - 1] += textPart.Text.Length;
            }
            //make simple approximation, we should never sort it
            int sumOfhalfList = textBoxSize.Sum() / 2;
            int counterSize = 0;
            int textBoxesIndex = textBoxSize.FindIndex(textSize =>
            {
                counterSize += textSize;
                if (counterSize >= sumOfhalfList)
                    return true;
                return false;
            });
            //split in two 
            pageSectionViewModel.otherTextBoxViewModelList = pageSectionViewModel.TextBoxViewModelList
                .GetRange(textBoxesIndex + 1, pageSectionViewModel.TextBoxViewModelList.Count - (textBoxesIndex + 1));

            pageSectionViewModel.TextBoxViewModelList = pageSectionViewModel.TextBoxViewModelList
               .GetRange(0, textBoxesIndex + 1);
        }

    }
}
