namespace PhluffyFotos.Web.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using PhluffyFotos.Data;

    public class AlbumUploadViewModel
    {
        public IEnumerable<Album> Albums { get; set; }

        [Required]
        public string Album { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }
    }
}
