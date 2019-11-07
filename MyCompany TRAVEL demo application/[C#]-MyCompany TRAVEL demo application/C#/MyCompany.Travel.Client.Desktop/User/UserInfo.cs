namespace MyCompany.Travel.Client.Desktop.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// User data.
    /// </summary>
    public class UserInfo
    {
        private static Employee current;

        private UserInfo()
        {

        }

        /// <summary>
        /// Access to current instance.
        /// </summary>
        public static Employee Current
        {
            get
            {
                if (current == null)
                    current = new Employee();

                return current;
            }

            set
            {
                current = value;
            }
        }
    }
}
