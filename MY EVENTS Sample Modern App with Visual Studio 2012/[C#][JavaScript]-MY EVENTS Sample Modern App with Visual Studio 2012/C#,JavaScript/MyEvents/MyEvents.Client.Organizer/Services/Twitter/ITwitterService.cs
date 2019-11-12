using System;
using System.Threading.Tasks;

namespace MyEvents.Client.Organizer.Services.Twitter
{
    /// <summary>
    /// Interface for twitter service.
    /// </summary>
    public interface ITwitterService
    {
        /// <summary>
        /// Initialize twitter service.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        /// <param name="Url"></param>
        void InitializeTwitterService(string eventName, string consumerKey, string consumerSecret, string Url);

        /// <summary>
        /// Check to see if we have granted access to twitter.
        /// </summary>
        bool AccessGranted { get; }

        /// <summary>
        /// Loged user screen name.
        /// </summary>
        string ScreenName { get; }

        /// <summary>
        /// If we dont have access to twitter we need to gain access to it
        /// </summary>
        /// <returns></returns>
        Task<Boolean> GainAccessToTwitter();

        /// <summary>
        /// Update our status (tweet)
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<Boolean> UpdateStatus(String status);

        /// <summary>
        /// Get our public timeline. returns JSon
        /// </summary>
        /// <returns></returns>
        Task<string> GetTimeline();
    }
}
