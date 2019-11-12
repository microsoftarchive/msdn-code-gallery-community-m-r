using System.Threading.Tasks;

namespace MyEvents.Client.Organizer.Services.Twitter
{
    /// <summary>
    /// ITwitterService implementation
    /// </summary>
    public class TwitterService : ITwitterService
    {
        private TwitterRt twitterRt;

        public TwitterService()
        {
        }

        public void InitializeTwitterService(string eventName, string consumerKey, string consumerSecret, string Url)
        {
            twitterRt = new TwitterRt(eventName, consumerKey, consumerSecret, Url);
        }
        public bool AccessGranted
        {
            get 
            {
                if (twitterRt != null)
                    return twitterRt.AccessGranted;

                return false;
            }
        }

        /// <summary>
        /// Loged user screen name.
        /// </summary>
        public string ScreenName 
        {
            get
            {
                if (twitterRt != null)
                    return twitterRt.ScreenName;

                return string.Empty;
            }
        }

        public async Task<bool> GainAccessToTwitter()
        {
            return await twitterRt.GainAccessToTwitter();
        }

        public async Task<bool> UpdateStatus(string status)
        {
            return await twitterRt.UpdateStatus(status);
        }

        public async Task<string> GetTimeline()
        {
            return await twitterRt.GetTimeline();
        }
    }
}
