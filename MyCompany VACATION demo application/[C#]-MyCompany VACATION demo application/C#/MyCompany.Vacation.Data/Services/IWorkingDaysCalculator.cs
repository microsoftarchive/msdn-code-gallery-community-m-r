
namespace MyCompany.Vacation.Data.Services
{
    using System;

    /// <summary>
    /// Working days calculator interface
    /// </summary>
    public interface IWorkingDaysCalculator
    {
        /// <summary>
        /// Gets the working days.
        /// </summary>
        /// <param name="officeId">The office id.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        int GetWorkingDays(int officeId, DateTime start, DateTime end);
    }
}