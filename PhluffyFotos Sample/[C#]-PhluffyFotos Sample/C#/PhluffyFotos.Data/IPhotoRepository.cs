namespace PhluffyFotos.Data
{
    using System.Collections.Generic;
    using System.IO;

    public interface IPhotoRepository
    {
        IEnumerable<Photo> GetPhotosByAlbum(string owner, string albumId);

        Photo GetPhotoByOwner(string owner, string albumId, string photoId);

        void Add(Photo photo, Stream binary, string mimeType, string name);
        
        void Delete(string owner, string album, string photoId);
        
        void UpdatePhotoData(Photo photo);
        
        void CreateTags(string photoId, Tag[] tags);
        
        void RemoveTags(string photoId, Tag[] tags);

        IEnumerable<Album> GetAlbums();
        
        IEnumerable<Album> GetAlbumsByOwner(string owner);

        void CreateAlbum(string albumName, string owner);
        
        void DeleteAlbum(string albumName, string owner);
        
        void UpdateAlbum(Album album);

        IEnumerable<Photo> FindPhotosByTag(params string[] tags);

        void BootstrapUser(string userName, string albumName);
    }
}
