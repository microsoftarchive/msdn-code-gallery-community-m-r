namespace MyCompany.Travel.Client.Desktop.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Pagination Manager class.
    /// </summary>
    public class PaginationConfig
    {
        private PageItem pageSelected;
        private int itemsCounted;
        private int pageSize;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="itemsCounted">Items counted.</param>
        /// <param name="pageSize">Size of the page.</param>
        public PaginationConfig(int itemsCounted, int pageSize)
        {
            this.itemsCounted = itemsCounted;
            this.pageSize = pageSize;
            this.pageSelected = new PageItem((1).ToString(), true, true);
        }

        /// <summary>
        /// Number of pages.
        /// </summary>
        public int NumberOfPages
        {
            get { return (int)Math.Ceiling((double)this.itemsCounted / this.pageSize); }
        }

        /// <summary>
        /// Page size.
        /// </summary>
        public int PageSize
        {
            get { return this.pageSize; }
            set { this.pageSize = value; }
        }

        /// <summary>
        /// Page selected.
        /// </summary>
        public PageItem PageSelected
        {
            get { return this.pageSelected; }
            set { this.pageSelected = value; }
        }

        /// <summary>
        /// Number of page selected.
        /// </summary>
        public int NumberOfPageSelected
        {
            get { return int.Parse(this.pageSelected.Page); }
            set { this.pageSelected.Page = value.ToString(); }
        }

        /// <summary>
        /// Items counted.
        /// </summary>
        public int ItemsCounted
        {
            get { return this.itemsCounted; }
            set
            { 
                this.itemsCounted = value;
            }
        }

        /// <summary>
        /// This property is true if user can go to the next page.
        /// </summary>
        public bool CanGoNext
        {
            get { return NumberOfPageSelected < NumberOfPages; }
        }

        /// <summary>
        /// This property is true if user can go to the previous page.
        /// </summary>
        public bool CanGoPrevious
        {
            get { return 1 < NumberOfPageSelected; }
        }
    }
}
