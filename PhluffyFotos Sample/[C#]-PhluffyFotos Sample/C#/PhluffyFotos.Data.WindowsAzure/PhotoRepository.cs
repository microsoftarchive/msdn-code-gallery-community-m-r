namespace PhluffyFotos.Data.WindowsAzure
{
    using System;
    using System.Collections.Generic;
    using System.Data.Services.Client;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Xml.Linq;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;
    using PhluffyFotos.Data;
    using PhluffyFotos.Data.Exceptions;

    public class PhotoRepository : IPhotoRepository
    {
        private CloudStorageAccount storageAccount;

        public PhotoRepository(CloudStorageAccount account)
        {
            this.storageAccount = account;
        }

        public PhotoRepository()
            : this(CloudStorageAccount.FromConfigurationSetting("DataConnectionString"))
        {
        }

        #region IPhotoRepository Members

        public IEnumerable<Photo> GetPhotosByAlbum(string owner, string albumId)
        {
            var context = new PhotoAlbumDataContext();

            // use this partial one to correctly construct partition keys
            var temp = new PhotoRow(new Photo { Owner = owner.ToLowerInvariant(), AlbumId = albumId });

            return context.Photos.Where(p => p.PartitionKey == temp.PartitionKey).AsTableServiceQuery()
                .AsEnumerable()
                .ToModel();

            // TODO: Handle Paging
        }

        public Photo GetPhotoByOwner(string owner, string albumId, string photoId)
        {
            var context = new PhotoAlbumDataContext();

            // use this partial one to correctly construct partition keys
            var temp = new PhotoRow(new Photo { Owner = owner.ToLowerInvariant(), AlbumId = albumId, PhotoId = photoId });

            // we add the 'true' predicate in order to not get 404 if photo is missing (we just want a null)
            return context.Photos.Where(p => p.PartitionKey == temp.PartitionKey && p.RowKey == temp.RowKey && true).AsTableServiceQuery()
                .AsEnumerable()
                .ToModel()
                .SingleOrDefault();
        }

        public void Add(Photo photo, Stream binary, string mimeType, string name)
        {
            // get just the file name and ignore the path
            var file = name.Substring(name.LastIndexOf("\\", StringComparison.OrdinalIgnoreCase) + 1);

            var context = new PhotoAlbumDataContext();

            try
            {
                // add the photo to table storage
                context.AddObject(PhotoAlbumDataContext.PhotoTable, new PhotoRow(photo));
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("EntityAlreadyExists"))
                {
                    throw new PhotoNameAlreadyInUseException(photo.AlbumId, photo.Title);
                }
                else
                {
                    throw;
                }
            }

            // add the binary to blob storage
            var storage = this.storageAccount.CreateCloudBlobClient();
            var container = storage.GetContainerReference(photo.Owner.ToLowerInvariant());
            container.CreateIfNotExist();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            var blob = container.GetBlobReference(file);

            blob.Properties.ContentType = mimeType;
            blob.UploadFromStream(binary);

            // post a message to the queue so it can process tags and the sizing operations
            this.SendToQueue(
                Constants.PhotoQueue,
                string.Format(CultureInfo.InvariantCulture, "{0}|{1}|{2}|{3}", photo.Owner, photo.AlbumId, photo.PhotoId, file));
        }

        public void UpdatePhotoData(Photo photo)
        {
            var context = new PhotoAlbumDataContext();
            var photoRow = new PhotoRow(photo);

            // attach and update the photo row
            context.AttachTo(PhotoAlbumDataContext.PhotoTable, photoRow, "*");
            context.UpdateObject(photoRow);
            context.SaveChanges();
        }

        public void UpdateAlbumData(string owner, string albumId, string thumbnailUrl)
        {
            var context = new PhotoAlbumDataContext();

            var albumRow = new AlbumRow(new Album() { AlbumId = albumId, Owner = owner });
            var album = context.Albums.Where(a => a.PartitionKey == albumRow.PartitionKey && a.RowKey == albumRow.RowKey && a.ThumbnailUrl == thumbnailUrl)
                .AsTableServiceQuery()
                .FirstOrDefault();

            if (album != null)
            {
                // get the first the photo in the album to update the thumbnail
                var photoRow = new PhotoRow(new Photo { Owner = owner.ToLowerInvariant(), AlbumId = albumId });
                var otherPhoto = context.Photos.Where(p => p.PartitionKey == photoRow.PartitionKey).Take(1).SingleOrDefault();

                // if the album is empty, we set the HasPhotos property to false to hide it from the UI
                if (otherPhoto != null)
                {
                    album.ThumbnailUrl = otherPhoto.ThumbnailUrl;
                }
                else
                {
                    album.ThumbnailUrl = string.Empty;
                    album.HasPhotos = false;
                }

                this.UpdateAlbum(album.ToModel());
            }
        }

        public void Delete(string owner, string album, string photoId)
        {
            var context = new PhotoAlbumDataContext();

            // we use this to help calculate partition keys, rowkeys in query later
            var temp = new PhotoRow(new Photo { PhotoId = photoId, AlbumId = album, Owner = owner.ToLowerInvariant() });

            // see if the photo exists
            var photo = context.Photos.Where(p => p.PartitionKey == temp.PartitionKey && p.RowKey == temp.RowKey && true).AsTableServiceQuery()
                .ToModel()
                .SingleOrDefault();

            if (photo != null)
            {
                this.Delete(photo);
            }
        }

        public void Delete(Photo photo)
        {
            var context = new PhotoAlbumDataContext();
            var photoRow = new PhotoRow(photo);

            context.AttachTo(PhotoAlbumDataContext.PhotoTable, photoRow, "*");
            context.DeleteObject(photoRow);
            context.SaveChanges();

            // tell the worker role to clean up blobs and tags
            this.SendToQueue(
                Constants.PhotoCleanupQueue,
                string.Format(CultureInfo.InvariantCulture, "{0}|{1}|{2}|{3}|{4}|{5}", photo.PhotoId, photo.Owner, photo.Url, photo.RawTags, photo.ThumbnailUrl, photo.AlbumId));
        }

        public void CreateTags(string photoId, Tag[] tags)
        {
            var context = new PhotoAlbumDataContext();

            foreach (var tag in tags)
            {
                // add the tag and associate to a picture for later searching
                context.AddObject(PhotoAlbumDataContext.TagTable, new TagRow(tag));
                context.AddObject(PhotoAlbumDataContext.PhotoTagTable, new PhotoTagRow(photoId, tag.Name));
            }

            try
            {
                // we want to continue - even if conflict is detected...
                context.SaveChanges(SaveChangesOptions.ContinueOnError);
            }
            catch (DataServiceRequestException ex)
            {
                foreach (var resp in ex.Response)
                {
                    // we might get a conflict here, which is ok (tag exists)
                    // the alternative is to query everytime to see if the tag
                    // exists, which is less efficient that just trying and 
                    // handling exception.
                    if (resp.StatusCode != (int)HttpStatusCode.Conflict
                        && resp.StatusCode != (int)HttpStatusCode.Created)
                    {
                        throw;
                    }
                }
            }
        }

        public void RemoveTags(string photoId, Tag[] tags)
        {
            var context = new PhotoAlbumDataContext();

            foreach (var tag in tags)
            {
                var photoTag = new PhotoTagRow(photoId, tag.Name);
                context.AttachTo(PhotoAlbumDataContext.PhotoTagTable, photoTag, "*");
                context.DeleteObject(photoTag);
            }

            try
            {
                // continue trying to delete, even if not found
                context.SaveChanges(SaveChangesOptions.ContinueOnError);
            }
            catch (DataServiceRequestException ex)
            {
                foreach (var resp in ex.Response)
                {
                    // to be more robust, we will ignore 404 errors when
                    // the entity might have already been deleted (due to an
                    // incomplete operation earliet).
                    if (resp.StatusCode != (int)HttpStatusCode.NotFound
                        && resp.StatusCode != (int)HttpStatusCode.OK)
                    {
                        throw;
                    }
                }
            }
        }

        public IEnumerable<Album> GetAlbums()
        {
            var context = new PhotoAlbumDataContext();

            return context.Albums.AsTableServiceQuery()
                .AsEnumerable()
                .ToModel();
        }

        public IEnumerable<Album> GetAlbumsByOwner(string owner)
        {
            var context = new PhotoAlbumDataContext();

            return context.Albums.Where(a => a.PartitionKey == owner).AsTableServiceQuery()
                .AsEnumerable()
                .ToModel();

            // TODO:  Implement paging
        }

        public void CreateAlbum(string albumName, string owner)
        {
            var context = new PhotoAlbumDataContext();

            var album = new Album
            {
                AlbumId = SlugHelper.GetSlug(albumName),
                Owner = owner.ToLowerInvariant(),
                Title = albumName
            };

            context.AddObject(PhotoAlbumDataContext.AlbumTable, new AlbumRow(album));
            context.SaveChanges();
        }

        public void DeleteAlbum(string albumName, string owner)
        {
            var context = new PhotoAlbumDataContext();

            // find the album by name and owner (we don't pass in ugly GUIDs for direct access
            var album = context.Albums
                .Where(a => a.AlbumId == albumName && a.PartitionKey == owner.ToLowerInvariant()).AsTableServiceQuery()
                .Single();

            context.DeleteObject(album);
            context.SaveChanges();

            // tell the worker role to clean up blobs and tags
            this.SendToQueue(
                Constants.AlbumCleanupQueue,
                string.Format(CultureInfo.InvariantCulture, "{0}|{1}", owner, album.AlbumId));
        }

        public void UpdateAlbum(Album album)
        {
            var context = new PhotoAlbumDataContext();
            var albumRow = new AlbumRow(album);

            // attach and update the photo row
            context.AttachTo(PhotoAlbumDataContext.AlbumTable, albumRow, "*");
            context.UpdateObject(albumRow);

            context.SaveChanges();
        }

        public IEnumerable<Photo> FindPhotosByTag(params string[] tags)
        {
            var context = new PhotoAlbumDataContext();

            // we have to dynamically build our query using an Expression tree
            Expression<Func<PhotoTagRow, bool>> search = null;
            foreach (var tag in tags)
            {
                var id = tag.Trim().ToLowerInvariant();

                if (string.IsNullOrEmpty(id))
                {
                    continue;
                }

                Expression<Func<PhotoTagRow, bool>> addendum = t => t.PartitionKey == id;

                if (search == null)
                {
                    search = addendum;
                }
                else
                {
                    search = Expression.Lambda<Func<PhotoTagRow, bool>>(Expression.OrElse(search.Body, addendum.Body), search.Parameters);
                }
            }

            // if we get back entries associated with the tag, we next have to query
            // to find the specific picture.
            if (search != null)
            {
                var rows = context.PhotoTags.Where(search).AsTableServiceQuery();

                Expression<Func<PhotoRow, bool>> photoPredicate = null;
                foreach (var row in rows)
                {
                    var id = row.PhotoId;
                    Expression<Func<PhotoRow, bool>> addendum = p => p.RowKey == id;
                    if (photoPredicate == null)
                    {
                        photoPredicate = addendum;
                    }
                    else
                    {
                        photoPredicate = Expression.Lambda<Func<PhotoRow, bool>>(Expression.OrElse(photoPredicate.Body, addendum.Body), photoPredicate.Parameters);
                    }
                }

                if (photoPredicate != null)
                {
                    return context.Photos.Where(photoPredicate).AsTableServiceQuery().ToModel();
                }
            }

            // by default, return an empty (non-null) enumeration
            return (new Photo[] { }).AsEnumerable();
        }

        public void BootstrapUser(string userName, string albumName)
        {
            // create the initial album for the user
            this.CreateAlbum(albumName, userName.ToLowerInvariant());

            // provision a container for the user's blobs
            var client = this.storageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference(userName.ToLowerInvariant());

            bool success = container.CreateIfNotExist();

            // set to public access
            container.SetPermissions(
                new BlobContainerPermissions()
                {
                    PublicAccess = BlobContainerPublicAccessType.Container
                });

            if (!success)
            {
                Trace.TraceError("Failed to create container for {0}", userName);
            }
        }

        #endregion

        private void SendToQueue(string queueName, string msg)
        {
            var queues = this.storageAccount.CreateCloudQueueClient();

            // TODO: add error handling and retry logic
            var q = queues.GetQueueReference(queueName);
            q.CreateIfNotExist();
            q.AddMessage(new CloudQueueMessage(msg));
        }
    }
}
