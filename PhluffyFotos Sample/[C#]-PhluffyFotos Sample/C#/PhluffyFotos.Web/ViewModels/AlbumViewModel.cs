namespace PhluffyFotos.Web.ViewModels
{
    using System.Collections.Generic;
    using PhluffyFotos.Data;

    public class AlbumViewModel
    {
        public IEnumerable<Album> Albums { get; set; }
        
        public string Owner { get; set; }
        
        public string Album { get; set; }
    }
}
