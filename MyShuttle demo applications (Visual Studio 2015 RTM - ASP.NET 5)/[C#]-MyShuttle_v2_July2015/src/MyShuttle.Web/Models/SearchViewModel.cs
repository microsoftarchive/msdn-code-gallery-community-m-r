namespace MyShuttle.Web.Models
{
    using Model;
    using System.Collections.Generic;

    public class SearchViewModel
    {
        public SearchViewModel()
        {
            SearchString = string.Empty;
        }
        
        public string SearchString { get; set; }
    }
}