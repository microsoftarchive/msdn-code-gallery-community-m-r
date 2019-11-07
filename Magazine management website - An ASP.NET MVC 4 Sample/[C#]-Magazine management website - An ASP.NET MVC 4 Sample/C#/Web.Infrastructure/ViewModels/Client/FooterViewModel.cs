namespace CIK.News.Web.Infras.ViewModels.Client
{
    using System.Collections.Generic;

    using CIK.News.Entities;
    using CIK.News.Entities.NewsAgg;

    public class FooterViewModel
    {
        public FooterViewModel()
        {
            this.Categories = new List<Category>();
        }

        public List<Category> Categories { get; set; }
    }
}