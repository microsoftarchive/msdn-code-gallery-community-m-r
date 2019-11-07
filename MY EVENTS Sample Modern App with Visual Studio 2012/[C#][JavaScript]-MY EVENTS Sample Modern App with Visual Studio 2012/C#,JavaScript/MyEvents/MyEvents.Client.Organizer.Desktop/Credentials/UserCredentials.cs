using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvents.Client.Organizer.Desktop.Credentials
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
        /// Facebook Id
        /// </summary>
        public string FacebookId { get; set; }

        /// <summary>
        /// My events user Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Facebook accessToken
        /// </summary>
        public string FacebookToken { get; set; }

        /// <summary>
        /// my events accessToken
        /// </summary>
        public string MyEventsToken { get; set; }

        /// <summary>
        /// User fullname
        /// </summary>
        public string FullName { get; set; }
    }
}
