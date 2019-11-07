namespace PhluffyFotos.Web.Controllers
{
    using System.Web.Mvc;
    using PhluffyFotos.Data;
    using PhluffyFotos.Data.WindowsAzure;
    using PhluffyFotos.Web.ViewModels;

    public class HomeController : Controller
    {
        private IPhotoRepository repository;

        public HomeController()
            : this(new PhotoRepository())
        {
        }

        public HomeController(IPhotoRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            AlbumViewModel albumViewModel = new AlbumViewModel();
            albumViewModel.Albums = this.repository.GetAlbums();
            return View(albumViewModel);
        }
    }
}