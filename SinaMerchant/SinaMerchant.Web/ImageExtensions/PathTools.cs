namespace BootCampCoreProject.Web.ImageExtensions
{
    public static class PathTools
    {
        public static string ProductImagePath = "/images/products/";
        public static string ProductImageUploadPath = Path.Join(Directory.GetCurrentDirectory(), "wwwroot/images/products/");

        public static string ProductImageThumbPath = "/images/products/thumb/";
        public static string ProductImageUploadThumbPath = Path.Join(Directory.GetCurrentDirectory(), "wwwroot/images/products/thumb/");
    }
}
