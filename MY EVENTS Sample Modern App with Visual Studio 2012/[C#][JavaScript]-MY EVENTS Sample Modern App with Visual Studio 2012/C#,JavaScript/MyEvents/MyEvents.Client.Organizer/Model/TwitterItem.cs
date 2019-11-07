
namespace MyEvents.Client.Organizer.Model
{
    /// <summary>
    /// Twitter entity.
    /// </summary>
    public class TwitterItem
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TwitterItem()
        {
        }

        /// <summary>
        /// timeline user that made the tweet.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("user")]
        public TwitterUser User { get; set; }

        /// <summary>
        /// tweet message.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("text")]
        public string Tweet { get; set; }
    }

    /// <summary>
    /// Represents a twitter user.
    /// </summary>
    public class TwitterUser
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TwitterUser()
        {
        }

        /// <summary>
        /// Twitter username
        /// </summary>
        [Newtonsoft.Json.JsonProperty("screen_name")]
        public string Username { get; set; }

        /// <summary>
        /// User fullname
        /// </summary>
        [Newtonsoft.Json.JsonProperty("name")]
        public string Fullname { get; set; }
    }
}
