namespace PhluffyFotos.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class AlbumCreateViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}
