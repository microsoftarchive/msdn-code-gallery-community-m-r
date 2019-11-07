namespace PhluffyFotos.Worker
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Diagnostics;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using Microsoft.WindowsAzure.StorageClient;
    using PhluffyFotos.Data;
    using PhluffyFotos.Data.WindowsAzure;

    public class WorkerRole : RoleEntryPoint
    {
        private CloudStorageAccount storageAccount;

        public WorkerRole()
        {
            // This code sets up a handler to update CloudStorageAccount instances when their corresponding
            // configuration settings change in the service configuration file.
            CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
            {
                // Provide the configSetter with the initial value
                configSetter(RoleEnvironment.GetConfigurationSettingValue(configName));

                RoleEnvironment.Changed += (sender, arg) =>
                {
                    if (arg.Changes.OfType<RoleEnvironmentConfigurationSettingChange>()
                        .Any((change) => (change.ConfigurationSettingName == configName)))
                    {
                        // The corresponding configuration setting has changed, propagate the value
                        if (!configSetter(RoleEnvironment.GetConfigurationSettingValue(configName)))
                        {
                            // In this case, the change to the storage account credentials in the
                            // service configuration is significant enough that the role needs to be
                            // recycled in order to use the latest settings. (for example, the 
                            // endpoint has changed)
                            RoleEnvironment.RequestRecycle();
                        }
                    }
                };
            });

            this.storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Justification = "Required to access Windows Azure Environment")]
        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // Windows Azure Logs
            var initialConfiguration = DiagnosticMonitor.GetDefaultInitialConfiguration(); 
            initialConfiguration.Logs.ScheduledTransferPeriod = TimeSpan.FromMinutes(5);
            DiagnosticMonitor.Start("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString", initialConfiguration);

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            RoleEnvironment.Changing += this.RoleEnvironmentChanging;

            return base.OnStart();
        }

        public override void Run()
        {
            var queueClient = this.storageAccount.CreateCloudQueueClient();

            int sleepTime = GetSleepTimeFromConfig();

            while (true)
            {
                Thread.Sleep(sleepTime);

                foreach (var q in queueClient.ListQueues())
                {
                    var msg = q.GetMessage();
                    var success = false;

                    if (msg != null)
                    {
                        var id = msg.Id;
                        switch (q.Name)
                        {
                            case Constants.PhotoQueue:
                                Trace.TraceInformation("Starting photo thumbnail and tagging");
                                success = this.CreateThumbnail(msg);
                                break;

                            case Constants.PhotoCleanupQueue:
                                Trace.TraceInformation("Starting photo removal and metadata cleanup");
                                success = this.CleanupPhoto(msg);
                                break;

                            case Constants.AlbumCleanupQueue:
                                Trace.TraceInformation("Starting album removal and metadata cleanup");
                                success = CleanupAlbum(msg);
                                break;

                            default:
                                Trace.TraceError("Unknown Queue found: {0}", q.Name);
                                break;
                        }

                        // remove the message if no error has occurred
                        // or delete if the message is poison (> 4 reads)
                        if (success || msg.DequeueCount > 4)
                        {
                            q.DeleteMessage(msg);
                        }
                    }
                }
            }
        }

        private static bool CleanupAlbum(CloudQueueMessage msg)
        {
            Trace.TraceInformation("CleanupAlbum called with {0}", msg.AsString);
            var parts = msg.AsString.Split('|');

            if (parts.Length != 2)
            {
                Trace.TraceError("Unexpected input to the album cleanup queue: {0}", msg.AsString);
                return false;
            }

            // interpret the message
            var owner = parts[0];
            var album = parts[1];

            var repository = new PhotoRepository();

            var photos = repository.GetPhotosByAlbum(owner, album);

            // this will trigger another message to the queue for more scale!
            foreach (var photo in photos)
            {
                repository.Delete(photo);
            }

            return true;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Justification = "Required to access Windows Azure Environment")]
        private static int GetSleepTimeFromConfig()
        {
            int sleepTime;

            if (!int.TryParse(RoleEnvironment.GetConfigurationSettingValue("WorkerSleepTime"), out sleepTime))
            {
                sleepTime = 0;
            }

            // polling less than a second seems too eager
            if (sleepTime < 1000)
            {
                sleepTime = 2000;
            }

            return sleepTime;
        }

        private void RoleEnvironmentChanging(object sender, RoleEnvironmentChangingEventArgs e)
        {
            // If a configuration setting is changing
            if (e.Changes.Any(change => change is RoleEnvironmentConfigurationSettingChange))
            {
                // Set e.Cancel to true to restart this role instance
                e.Cancel = true;
            }
        }

        private bool CleanupPhoto(CloudQueueMessage msg)
        {
            Trace.TraceInformation("CleanupPhoto called with {0}", msg.AsString);
            var parts = msg.AsString.Split('|');

            if (parts.Length != 6)
            {
                Trace.TraceError("Unexpected input to the photo cleanup queue: {0}", msg.AsString);
                return false;
            }

            try
            {
                // interpret the string
                var photoid = parts[0];
                var owner = parts[1];
                var url = parts[2];
                var rawTags = parts[3];
                var thumbnailUrl = parts[4];
                var albumId = parts[5];

                // the photoRow is already deleted by the frontend to remove from view
                // now we need to clean the binaries and the tag information
                var repository = new PhotoRepository();

                repository.UpdateAlbumData(owner, albumId, thumbnailUrl);

                // this cleans up the tag to photo relationship.  we will intentionally not
                // remove the tag however in here since it doesn't matter
                var tags = rawTags.Split(';')
                    .Where(s => s.Trim().Length > 0)
                    .Select(s => new Tag() { Name = s.Trim().ToLowerInvariant() })
                    .ToArray();

                repository.RemoveTags(photoid, tags);

                // next, let's remove the blobs from storage
                var filename = Path.GetFileName(url);
                var thumbname = Path.Combine("thumb", filename);

                if (!string.IsNullOrEmpty(filename))
                {
                    Trace.TraceWarning("Attempting to delete {0}", filename);

                    var client = this.storageAccount.CreateCloudBlobClient();
                    var container = client.GetContainerReference(owner);

                    var blobGone = container.GetBlobReference(filename).DeleteIfExists();
                    var thumbGone = container.GetBlobReference(thumbname).DeleteIfExists();

                    if (!blobGone || !thumbGone)
                    {
                        Trace.TraceWarning(string.Format(CultureInfo.InvariantCulture, "Failed to {0}", blobGone ? "Kill Thumb" : thumbGone ? "Kill both" : "Kill blob"));
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Cleanup Photo Failure");
                Trace.TraceError(ex.ToString());
                return false;
            }

            return true;
        }

        private bool CreateThumbnail(CloudQueueMessage msg)
        {
            Trace.TraceInformation("CreateThumbnail called with {0}", msg.AsString);
            var parts = msg.AsString.Split('|');

            if (parts.Length != 4)
            {
                Trace.TraceError("Unexpected input to the photo cleanup queue: {0}", msg.AsString);
                return false;
            }

            // interpret the string
            var owner = parts[0];
            var albumName = parts[1];
            var photoid = parts[2];
            var file = parts[3];

            var repository = new PhotoRepository();
            var photo = repository.GetPhotoByOwner(owner, albumName, photoid);

            if (photo != null)
            {
                // create the thumbnail
                try
                {
                    this.CreateThumb(owner, file);
                }
                catch (Exception ex)
                {
                    // creating the thumbnail failed for some reason
                    Trace.TraceError("CreateThumb failed for {0} and {1}", owner, file);
                    Trace.TraceError(ex.ToString());

                    return false; // bail out
                }

                // update table storage with blob data URLs
                var client = this.storageAccount.CreateCloudBlobClient();
                var blobUri = client.GetContainerReference(owner).GetBlobReference(file).Uri.ToString();
                var thumbUri = client.GetContainerReference(owner).GetBlobReference(Path.Combine("thumb", file)).Uri.ToString();

                // update the photo entity with thumb and blob location
                photo.ThumbnailUrl = thumbUri;
                photo.Url = blobUri;

                repository.UpdatePhotoData(photo);

                var album = repository.GetAlbumsByOwner(owner).Single(a => a.AlbumId == albumName);
                if (!album.HasPhotos || string.IsNullOrEmpty(album.ThumbnailUrl))
                {
                    // update the album
                    album.HasPhotos = true;
                    album.ThumbnailUrl = photo.ThumbnailUrl;

                    repository.UpdateAlbum(album);
                }

                // parse the tags and save them off
                var tags = photo.RawTags.Split(';')
                    .Where(s => s.Trim().Length > 0)
                    .Select(s => new Tag { Name = s.Trim().ToLowerInvariant() })
                    .ToArray();

                repository.CreateTags(photoid, tags);

                // TODO, aggregate stats
                return true;
            }

            // default
            Trace.TraceError("CreateThumbnail error, cannot find {0}", photoid);
            return false;
        }

        private void CreateThumb(string owner, string file)
        {
            var client = this.storageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference(owner);

            using (var ms = new MemoryStream())
            {
                container.GetBlobReference(file).DownloadToStream(ms);

                var image = Image.FromStream(ms);

                // calculate a 150px thumbnail
                int width;
                int height;
                if (image.Width > image.Height)
                {
                    width = 150;
                    height = 150 * image.Height / image.Width;
                }
                else
                {
                    height = 150;
                    width = 150 * image.Width / image.Height;
                }

                // generate the thumb
                var thumb = image.GetThumbnailImage(
                    width,
                    height,
                    () => false,
                    IntPtr.Zero);

                // save it off to blob storage
                using (var thumbStream = new MemoryStream())
                {
                    thumb.Save(
                        thumbStream,
                        System.Drawing.Imaging.ImageFormat.Jpeg);

                    thumbStream.Position = 0; // reset;

                    var thumbBlob = container.GetBlobReference(Path.Combine("thumb", file));
                    thumbBlob.Properties.ContentType = "image/jpeg";
                    thumbBlob.UploadFromStream(thumbStream);
                }

                Trace.TraceInformation("Thumbs for {0} created", file);
            }
        }
    }
}
