namespace PhluffyFotos.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using PhluffyFotos.Data;
    using PhluffyFotos.Data.WindowsAzure;

    public class PhotoController : Controller
    {
        private IPhotoRepository repository;

        public PhotoController()
            : this(new PhotoRepository())
        {
        }

        public PhotoController(IPhotoRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index(string owner, string album, string photoId)
        {
            var photo = this.repository.GetPhotoByOwner(owner, album, photoId);

            return View(photo);
        }

        public ActionResult Delete(string owner, string album, string photoId)
        {
            if (User.IsInRole("Administrator") || User.Identity.Name.Equals(owner, StringComparison.OrdinalIgnoreCase))
            {
                this.repository.Delete(owner, album, photoId);
            }

            return RedirectToAction("Get", "Album", new { owner, album });
        }
    }
}
