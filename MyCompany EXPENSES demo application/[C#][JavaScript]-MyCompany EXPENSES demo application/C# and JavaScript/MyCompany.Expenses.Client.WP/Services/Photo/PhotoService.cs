namespace MyCompany.Expenses.Client.WP.Services.Photo
{
    using Microsoft.Phone.Tasks;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation of photo contract
    /// </summary>
    public class PhotoService : IPhotoService
    {
        /// <summary>
        /// Take a photo or choose from the gallery.
        /// </summary>
        /// <returns></returns>
        public Task<byte[]> GetPhotoAsync()
        {
            var completitionSource = new TaskCompletionSource<byte[]>();

            PhotoChooserTask task = new PhotoChooserTask();
            task.Completed += (s, e) =>
            {
                if ((e.Error == null) && (e.ChosenPhoto != null))
                {
                    byte[] photoBytes = new byte[e.ChosenPhoto.Length];
                    e.ChosenPhoto.Read(photoBytes, 0, (int)e.ChosenPhoto.Length);
                    completitionSource.SetResult(photoBytes);
                }
            };

            task.ShowCamera = true;
            task.Show();

            return completitionSource.Task;
        }
    }
}
