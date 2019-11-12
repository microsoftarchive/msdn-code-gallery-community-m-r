namespace MyCompany.Visitors.Client.WP.Services.Photo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Contract for photo service.
    /// </summary>
    public interface IPhotoService
    {
        /// <summary>
        /// Take a photo or choose from the gallery.
        /// </summary>
        /// <returns></returns>
        Task<byte[]> GetPhoto();
    }
}
