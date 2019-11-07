namespace CIK.News.Web.Infras.MediaItem
{
    using System.IO;

    public interface IMediaItemStorage
    {
        string Storage(MemoryStream stream, string fileName);
    }
}