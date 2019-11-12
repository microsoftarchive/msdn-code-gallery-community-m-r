namespace MyCompany.Travel.Client.Desktop.Model
{
    using System;

    /// <summary>
    /// Page item class
    /// </summary>
    public class PageItem
    {
        private string page;
        private bool isCurrentPage;
        private bool isClickable;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="page">Number of page</param>
        /// <param name="isCurrentPage">Is current page</param>
        /// <param name="isClickable">Is clickable page</param> 
        public PageItem(string page, bool isCurrentPage, bool isClickable)
        {
            this.page = page;
            this.isCurrentPage = isCurrentPage;
            this.isClickable = isClickable;
        }

        /// <summary>
        /// Number of page.
        /// </summary>
        public string Page
        { 
            get { return this.page; }
            set { this.page = value; }
        }

        /// <summary>
        /// Is current page.
        /// </summary>
        public bool IsCurrentPage
        { 
            get { return this.isCurrentPage; }
            set
            {
                this.isCurrentPage = value;
                if (value)
                {
                    var tmp = PageSelected;
                    if (tmp != null)
                        tmp(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Is clickable.
        /// </summary>
        public bool IsClickable
        {
            get { return this.isClickable; }
            set { this.isClickable = value; }
        }

        /// <summary>
        /// Page selected event.
        /// </summary>
        public event EventHandler PageSelected;
    }
}
