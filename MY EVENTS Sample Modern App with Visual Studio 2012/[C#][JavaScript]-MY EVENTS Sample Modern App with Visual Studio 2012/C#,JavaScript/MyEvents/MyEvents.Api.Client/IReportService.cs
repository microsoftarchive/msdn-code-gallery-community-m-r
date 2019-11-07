using System;
using System.Collections.Generic;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// Class to access to the Report Controller exposed by MyEvents.API
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Get Top tags used in all the events that are in My Events Platform
        /// </summary>
        /// <param name="organizerId">Id of the organizer to get reports</param>
        /// <param name="callback">CallBack func to get tags</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetTopTagsAsync(int organizerId, Action<IList<Tag>> callback);

        /// <summary>
        /// Get Top speaker in all the events that are in My Events Platform
        /// </summary>
        /// <param name="organizerId">Id of the organizer to get reports</param>
        /// <param name="callback">CallBack func to get speakers</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetTopSpeakersAsync(int organizerId, Action<IList<Speaker>> callback);
    }
}
