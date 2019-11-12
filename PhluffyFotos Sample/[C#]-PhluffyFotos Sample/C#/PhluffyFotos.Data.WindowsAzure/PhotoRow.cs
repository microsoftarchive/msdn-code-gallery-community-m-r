namespace PhluffyFotos.Data.WindowsAzure
{
    using System;
    using System.Globalization;
    using Microsoft.WindowsAzure.StorageClient;

    public class PhotoRow : TableServiceEntity
    {
        public PhotoRow()
            : base()
        {
        }

        public PhotoRow(Photo photo)
            : base(string.Format(CultureInfo.InvariantCulture, "{0}_{1}", photo.Owner, photo.AlbumId), photo.PhotoId)
        {
            this.PhotoId = photo.PhotoId;
            this.Owner = photo.Owner;
            this.Title = photo.Title;
            this.Description = photo.Description;
            this.AlbumId = photo.AlbumId;
            this.RawTags = photo.RawTags;
            this.ThumbnailUrl = photo.ThumbnailUrl;
            this.Url = photo.Url;
        }

        private PhotoRow(string partitionKey, string rowKey)
            : base(partitionKey, rowKey)
        {
        }

        public string PhotoId { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public string Url { get; set; }
        
        public string ThumbnailUrl { get; set; }
        
        public string AlbumId { get; set; }
        
        public string Owner { get; set; }
        
        public string RawTags { get; set; }
    }
}
