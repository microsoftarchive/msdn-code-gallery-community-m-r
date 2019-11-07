namespace PhluffyFotos.Data
{
    public class Album
    {
        public string AlbumId { get; set; }

        public string Title { get; set; }
        
        public string Owner { get; set; }
        
        public string ThumbnailUrl { get; set; }
        
        public bool HasPhotos { get; set; }
    }
}
