namespace MyCompany.Vacation.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Employee information and her list of vacation request
    /// </summary>
    public class EmployeeVacationRequests
    {
        /// <summary>
        /// Gets or sets the list of vacation request
        /// </summary>
        public IList<VacationRequest> VacationRequests { get; set; }

        /// <summary>
        /// Gets or sets the employee
        /// </summary>
        public Employee Employee { get; set; }
    }
}