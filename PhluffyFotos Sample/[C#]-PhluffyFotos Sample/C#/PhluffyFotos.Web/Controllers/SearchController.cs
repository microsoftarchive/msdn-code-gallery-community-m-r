namespace PhluffyFotos.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using PhluffyFotos.Data;
    using PhluffyFotos.Data.WindowsAzure;

    public class SearchController : Controller
    {
        private IPhotoRepository repository;

        public SearchController()
            : this(new PhotoRepository())
        {
        }

        public SearchController(IPhotoRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Search(string tag)
        {
            var results = this.repository.FindPhotosByTag(tag);

            return View(results);
        }

        public ActionResult Tag(string searchTerms)
        {
            // get all the tags
            var tags = searchTerms.Split(';')
                .Select(s => s.Trim().ToLowerInvariant())
                .ToArray();

            var results = this.repository.FindPhotosByTag(tags);

            return View("Search", results);
        }
    }
}
