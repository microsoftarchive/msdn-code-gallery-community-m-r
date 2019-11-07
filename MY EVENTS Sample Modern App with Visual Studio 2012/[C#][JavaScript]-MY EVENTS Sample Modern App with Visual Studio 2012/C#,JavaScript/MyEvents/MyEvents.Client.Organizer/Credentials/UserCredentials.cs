
namespace MyEvents.Client.Organizer.Credentials
{
    /// <summary>
    /// User credentials.
    /// </summary>
    public class UserCredentials
    {
        private static UserCredentials current;

        private UserCredentials()
        { 
        
        }

        /// <summary>
        /// Access to current instance.
        /// </summary>
        public static UserCredentials Current
        {
            get
            {
                if (current == null)
                    current = new UserCredentials();
                return current;
            }
        }

        /// <summary>
        /// Current user.
        /// </summary>
        public User CurrentUser { get; set; }
    }

    /// <summary>
    /// User definition
    /// </summary>
    public class User
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public User()
        {
        }

        public string FacebookId { get; set; }
        
        public int UserId { get; set; }

        public string FacebookToken { get; set; }

        public string MyEventsToken { get; set; }

        public string FullName { get; set; }

        public string UserMain { get; set; }

        public string Bio { get; set; }

        public string Location { get; set; }


    }
}
