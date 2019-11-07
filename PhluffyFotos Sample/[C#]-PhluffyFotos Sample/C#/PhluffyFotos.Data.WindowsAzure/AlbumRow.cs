namespace PhluffyFotos.Data.WindowsAzure
{
    using Microsoft.WindowsAzure.StorageClient;

    public class AlbumRow : TableServiceEntity
    {
        public AlbumRow()
            : base()
        {
        }

        public AlbumRow(Album album)
            : this(album.Owner, album.AlbumId.ToLowerInvariant())
        {
            this.AlbumId = album.AlbumId;
            this.Title = album.Title;
            this.Owner = album.Owner;
            this.ThumbnailUrl = album.ThumbnailUrl;
            this.HasPhotos = album.HasPhotos;
        }

        private AlbumRow(string partitionKey, string rowKey)
            : base(partitionKey, rowKey)
        {
        }

        public string AlbumId { get; set; }
        
        public string Title { get; set; }
        
        public string Owner { get; set; }
        
        public string ThumbnailUrl { get; set; }
        
        public bool HasPhotos { get; set; }
    }
}
