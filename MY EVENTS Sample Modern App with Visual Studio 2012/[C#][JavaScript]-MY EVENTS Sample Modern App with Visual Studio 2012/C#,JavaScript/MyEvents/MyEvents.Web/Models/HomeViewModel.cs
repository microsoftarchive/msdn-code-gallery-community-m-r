using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyEvents.Model;

namespace MyEvents.Web.Models
{
    /// <summary>
    /// Home View Model.
    /// </summary>
    public class HomeViewModel
    {
        private const int NumberOfComingSoon = 6;
        private const int NumberOfHighlighted = 3;

        /// <summary>
        /// The home view model construtor.
        /// </summary>
        /// <param name="events"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public HomeViewModel(IEnumerable<EventDefinition> events, int totalCount, int pageIndex, int pageSize)
        {
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.TotalCount = totalCount;
            this.Events = events;
        }

        /// <summary>
        /// Coming Soon Highlighted
        /// </summary>
        public IEnumerable<EventDefinition> ComingSoonHighlighted
        {
            get
            {
                return Events.Take(NumberOfHighlighted);
            }
        }

        /// <summary>
        /// Coming Soon
        /// </summary>
        public IEnumerable<EventDefinition> ComingSoon
        {
            get
            {
                return Events.Skip(NumberOfHighlighted).Take(NumberOfComingSoon);
            }
        }

        /// <summary>
        /// The event list for the current user.
        /// </summary>
        public IEnumerable<EventDefinition> Events { get; private set; }

        /// <summary>
        /// The registers total count.
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// The total number of pages.
        /// </summary>
        public int TotalPages { get { return (int)Math.Ceiling(this.TotalCount / (double)this.PageSize); } }

        /// <summary>
        /// The current page index.
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// The page size.
        /// </summary>
        public int PageSize { get; private set; }
    }
}