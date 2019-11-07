
namespace MyCompany.Travel.Client
{
    using System.Collections.Generic;

    /// <summary>
    /// Travel Distribution
    /// </summary>
    public class TravelDistribution
    {
        /// <summary>
        /// City (travel destination)
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Number of travels in the last year
        /// </summary>
        public int YearCount { get; set; }

        /// <summary>
        /// Number of travels in the last month
        /// </summary>
        public int MonthCount { get; set; } 

        /// <summary>
        /// percent from total travels
        /// </summary>
        public decimal Percent { get; set; }

        /// <summary>
        /// Pictures of employees in those travels
        /// </summary>
        public List<EmployeePicture> EmployeesPictures { get; set; }
    }
}
