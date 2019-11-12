namespace MyCompany.Travel.Client.Desktop.Services.SampleData
{
    using MyCompany.Travel.Client.Desktop.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Sample data service implementation
    /// </summary>
    public class SampleDataService : ISampleDataService
    {
        /// <summary>
        /// Gets sample travel requests.
        /// </summary>
        /// <returns></returns>
        public List<TravelRequest> GetTravelRequests()
        {
            var travelRequests = new List<TravelRequest>();

            for (int i = 0; i < 10; i++)
            {
                TravelRequest travelRequest = new TravelRequest
                {
                    Employee = new Employee { FirstName = "Adam", LastName = "Barr", JobTitle = "Developer" },
                    Depart = DateTime.Now,
                    To = "New York",
                    From = "Bilbao",
                    TravelType = TravelType.Roundtrip,
                    RelatedProject = "MyCompany"
                };

                travelRequests.Add(travelRequest);
            }

            return travelRequests;
        }

        /// <summary>
        /// Gets an example of pages.
        /// </summary>
        /// <returns></returns>
        public List<PageItem> GetPagesList()
        {
            var pagesList = new List<PageItem>();
            pagesList.Add(new PageItem((1).ToString(), true, true));
            for (int i = 2; i <= 5; i++)
            {
                pagesList.Add(new PageItem((i).ToString(), false, true));
            }

            return pagesList;
        }
    }
}
