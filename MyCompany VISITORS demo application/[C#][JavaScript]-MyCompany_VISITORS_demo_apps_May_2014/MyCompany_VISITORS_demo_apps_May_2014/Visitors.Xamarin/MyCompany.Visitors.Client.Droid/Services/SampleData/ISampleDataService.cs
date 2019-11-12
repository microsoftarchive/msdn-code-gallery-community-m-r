
namespace MyCompany.Visitors.Client.WindowsStore.Services.SampleData
{
    using System.Collections.Generic;

    /// <summary>
    /// Sample data contract.
    /// </summary>
    public interface ISampleDataService
    {
        /// <summary>
        /// Sample visits.
        /// </summary>
        /// <returns>Visit list</returns>
        List<Visit> GetVisits(int n);

        /// <summary>
        /// Sample employees.
        /// </summary>
        /// <returns>Employees list</returns>
        List<Employee> GetEmployees();

        /// <summary>
        /// Sample visitors.
        /// </summary>
        /// <returns>Visitors list</returns>
        List<Visitor> GetVisitors();
    }
}
