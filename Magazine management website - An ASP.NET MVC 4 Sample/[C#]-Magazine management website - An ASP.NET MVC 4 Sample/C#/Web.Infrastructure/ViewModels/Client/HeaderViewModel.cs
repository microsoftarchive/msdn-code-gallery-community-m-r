namespace CIK.News.Web.Infras.ViewModels.Client
{
    using System.Collections.Generic;

    using CIK.News.Entities;
    using CIK.News.Entities.NewsAgg;

    public class HeaderViewModel
    {
        public HeaderViewModel()
        {
            this.Categories = new List<Category>();
        }

        public string SiteTitle { get; set; }

        public int CurrentCategoryId { get; set; }

        public List<Category> Categories { get; set; }
    }
}