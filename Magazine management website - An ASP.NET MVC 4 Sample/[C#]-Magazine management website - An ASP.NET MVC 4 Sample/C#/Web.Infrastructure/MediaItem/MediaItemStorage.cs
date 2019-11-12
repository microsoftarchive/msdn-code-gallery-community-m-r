namespace CIK.News.Web.Infras.MediaItem
{
    using System.Configuration;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Web;

    using CIK.News.Framework.Extensions;

    public class MediaItemStorage : IMediaItemStorage
    {
        public string Storage(MemoryStream stream, string fileName)
        {
            // TODO: should remove dependency on ConfigurationManager
            var virtualPath = ConfigurationManager.AppSettings["MediaItemPath"].ToString(CultureInfo.InvariantCulture);
            
            var physicalPath = HttpContext.Current.Request.MapPath(virtualPath);

            if (string.IsNullOrEmpty(physicalPath))
                throw new NoNullAllowedException("Physical path should not be null");

            var fullPath = Path.Combine(physicalPath, fileName);

            stream.WriteTo(fullPath);

            return fileName;
        }
    }
}