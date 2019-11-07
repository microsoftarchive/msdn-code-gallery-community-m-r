namespace MyCompany.Visitors.Client.WP.Services.Storage
{
    using MyCompany.Visitors.Client.WP.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Contract for storage service.
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Load personal information from isolated storage.
        /// </summary>
        /// <returns></returns>
        Visitor LoadPersonalInformation();

        /// <summary>
        /// Save personal information to isolated storage.
        /// </summary>
        /// <param name="pInformation">User defined personal information</param>
        /// <returns></returns>
        void SavePersonalInformation(Visitor pInformation);

        /// <summary>
        /// Known if the user had saved information.
        /// </summary>
        /// <returns></returns>
        bool ExistInformation();
    }
}
