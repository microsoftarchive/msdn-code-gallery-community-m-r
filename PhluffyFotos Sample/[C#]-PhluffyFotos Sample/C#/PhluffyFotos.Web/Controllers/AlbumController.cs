namespace PhluffyFotos.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using PhluffyFotos.Data;
    using PhluffyFotos.Data.Exceptions;
    using PhluffyFotos.Data.WindowsAzure;
    using PhluffyFotos.Web.ViewModels;

    public class AlbumController : Controller
    {
        private IPhotoRepository repository;

        public AlbumController()
            : this(new PhotoRepository())
        {
        }

        public AlbumController(IPhotoRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            AlbumViewModel albumViewModel = new AlbumViewModel();

            albumViewModel.Albums = this.repository.GetAlbums();

            return View(albumViewModel);
        }

        public ActionResult Get(string owner, string album)
        {
            if (string.IsNullOrEmpty(album))
            {
                return RedirectToAction("MyAlbums", new { owner = owner });
            }

            AlbumViewModel albumViewModel = new AlbumViewModel();
            PhotoViewModel photoViewModel = new PhotoViewModel();

            albumViewModel.Album = album;
            photoViewModel.Album = album;
            albumViewModel.Owner = owner.ToLowerInvariant();
            photoViewModel.Owner = owner.ToLowerInvariant();

            var albums = this.repository.GetAlbumsByOwner(owner.ToLowerInvariant());
            var foundAlbum = albums.SingleOrDefault(a => a.AlbumId == album || a.Title == album);

            if (foundAlbum != null)
            {
                photoViewModel.Photos = this.repository.GetPhotosByAlbum(owner, foundAlbum.AlbumId);
                return View(photoViewModel);
            }

            // no album found
            return View(new PhotoViewModel());
        }

        public ActionResult MyAlbums(string owner)
        {
            AlbumViewModel albumViewModel = new AlbumViewModel();
            PhotoViewModel photoViewModel = new PhotoViewModel();

            albumViewModel.Owner = owner.ToLowerInvariant();
            photoViewModel.Owner = owner.ToLowerInvariant();

            var albums = this.repository.GetAlbumsByOwner(owner.ToLowerInvariant());

            // if there are more than 1 album with photos for this user
            // then return a view of the albums for them
            if (albums.Where(a => a.HasPhotos).Count() > 1)
            {
                albumViewModel.Albums = albums;
                return View("Index", albumViewModel);
            }

            // if there is only 1 album, just return photos for it
            if (albums.Where(a => a.HasPhotos).Count() == 1)
            {
                photoViewModel.Photos = this.repository.GetPhotosByAlbum(owner, albums.First().AlbumId);
                return View("Get", photoViewModel);
            }

            // there are no albums for this user
            return View("Get", new PhotoViewModel());
        }

        [Authorize]
        public ActionResult Create()
        {
            return View(new AlbumCreateViewModel()
            {
                Title = "My Pics"
            });
        }

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult Create(AlbumCreateViewModel album)
        {
            if (!ModelState.IsValid)
            {
                return View(album);
            }

            var owner = User.Identity.Name.ToLowerInvariant();
            if (this.repository.GetAlbumsByOwner(owner).FirstOrDefault(o => o.AlbumId == SlugHelper.GetSlug(album.Title)) != null)
            {
                ModelState.AddModelError("Title", "An album with the same name already exists.");
                return View(album);
            }

            this.repository.CreateAlbum(album.Title, owner);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Upload()
        {
            AlbumUploadViewModel albumViewModel = new AlbumUploadViewModel
            {
                Title = "Untitled"
            };

            albumViewModel.Albums = this.repository.GetAlbumsByOwner(User.Identity.Name.ToLowerInvariant());

            if (albumViewModel.Albums.Count() > 0)
            {
                return View(albumViewModel);
            }

            return RedirectToAction("Create");
        }

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult Upload(AlbumUploadViewModel model)
        {
            if (Request.Files.Count != 1 || Request.Files[0].ContentLength == 0)
            {
                ModelState.AddModelError("Photo", "Provide a photo");
            }

            if (!ModelState.IsValid)
            {
                model.Albums = this.repository.GetAlbumsByOwner(User.Identity.Name.ToLowerInvariant());
                return View(model);
            }

            var photo = Request.Files[0];

            // generate a unique name
            string id = SlugHelper.GetSlug(model.Title);
            string owner = User.Identity.Name.ToLowerInvariant();
            string albumName = model.Album;

            try
            {
                this.repository.Add(
                                    new Photo()
                                    {
                                        AlbumId = albumName,
                                        Description = model.Description,
                                        Owner = owner,
                                        PhotoId = id,
                                        Title = model.Title,
                                        ThumbnailUrl = "/Content/images/processing.png",
                                        Url = "/Content/images/processing.png",
                                        RawTags = string.IsNullOrEmpty(model.Tags) ? "uncategorized" : model.Tags
                                    },
                                    photo.InputStream,
                                    photo.ContentType,
                                    photo.FileName);
            }
            catch (PhotoNameAlreadyInUseException)
            {
                ModelState.AddModelError("Title", "A picture with the same name already exists on this album");
                model.Albums = this.repository.GetAlbumsByOwner(User.Identity.Name.ToLowerInvariant());
                return View(model);
            }

            return RedirectToAction("Index", "Photo", new { owner = owner, album = model.Album, photoId = id });
        }

        public ActionResult Delete(string owner, string album)
        {
            if (User.IsInRole("Administrator") || User.Identity.Name.Equals(owner, StringComparison.OrdinalIgnoreCase))
            {
                this.repository.DeleteAlbum(album, owner);
            }

            return RedirectToAction("MyAlbums", new { owner = owner });
        }
    }
}
