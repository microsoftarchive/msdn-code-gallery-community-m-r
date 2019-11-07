namespace CIK.News.Web.Infras.Extensions
{
    using System.IO;
    using System.Web;

    using CIK.News.Web.Infras.MediaItem;

    public static class HttpPostedFileExtension
    {
        public static string CreateImagePathFromStream(this HttpPostedFileBase postedFile, IMediaItemStorage imageStorage)
        {
            var imagePath = string.Empty;

            if (postedFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    postedFile.InputStream.CopyTo(memoryStream);

                    imagePath = imageStorage.Storage(memoryStream, postedFile.FileName);
                }
            }

            return imagePath;
        }
    }
}