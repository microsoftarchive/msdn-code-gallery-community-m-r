namespace PhluffyFotos.Data.WindowsAzure
{
    using System;
    using System.Globalization;
    using Microsoft.WindowsAzure.StorageClient;

    public class PhotoTagRow : TableServiceEntity
    {
        public PhotoTagRow()
            : base()
        {
        }

        public PhotoTagRow(string photoId, string tag) : 
            base(tag, string.Format(CultureInfo.InvariantCulture, "{0}_{1}", tag, photoId))
        {
            this.PhotoId = photoId;
            this.Tag = tag;
        }

        public string PhotoId { get; set; }

        public string Tag { get; set; }
    }
}
