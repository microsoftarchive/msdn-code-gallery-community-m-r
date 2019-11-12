namespace PhluffyFotos.Web.ViewModels
{
    using System.Collections.Generic;
    using PhluffyFotos.Data;

    public class PhotoViewModel
    {
        public IEnumerable<Photo> Photos { get; set; }
        
        public string Owner { get; set; }
        
        public string Album { get; set; }
    }
}
