namespace MyCompany.Travel.Client.Desktop.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Loading Message class.
    /// </summary>
    public class LoadingMessage
    {
        private bool isLoading;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="isLoading">Is loading</param>
        public LoadingMessage(bool isLoading)
        {
            this.isLoading = isLoading;
        }

        /// <summary>
        /// Is loading property.
        /// </summary>
        public bool IsLoading
        {
            get { return this.isLoading; }
            set { this.isLoading = value; }
        }
    }
}
