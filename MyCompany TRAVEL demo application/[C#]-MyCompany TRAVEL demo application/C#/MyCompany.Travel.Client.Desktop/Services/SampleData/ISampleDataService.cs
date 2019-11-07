namespace MyCompany.Travel.Client.Desktop.Services.SampleData
{
    using MyCompany.Travel.Client.Desktop.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Sample data service contract.
    /// </summary>
    public interface ISampleDataService
    {
        /// <summary>
        /// Gets sample travel requests.
        /// </summary>
        /// <returns></returns>
        List<TravelRequest> GetTravelRequests();

        /// <summary>
        /// Gets an example of pages.
        /// </summary>
        /// <returns></returns>
        List<PageItem> GetPagesList();
    }
}
