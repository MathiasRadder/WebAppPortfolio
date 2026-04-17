namespace WebPortfolio.Services
{
    public class HelperService
    {
        private const string folderImage = "images";
        private const string imageType = "png";
        private const string iconFolderSection = "Icons";

        public static string? CreateImageLocation(string folderSection, string? ImageName, string? imagefileType)
        {
            if (string.IsNullOrWhiteSpace(ImageName))
                return null;

            if (imagefileType == null)
                return $"/{folderImage}/{folderSection}/{ImageName}.{imageType}";
            return $"/{folderImage}/{folderSection}/{ImageName}.{imagefileType}";
        }

        public static string? CreateImageIconLocation(string? ImageName, string? imagefileType)
        {
            return CreateImageLocation(iconFolderSection, ImageName, imagefileType);
        }
    }
}
