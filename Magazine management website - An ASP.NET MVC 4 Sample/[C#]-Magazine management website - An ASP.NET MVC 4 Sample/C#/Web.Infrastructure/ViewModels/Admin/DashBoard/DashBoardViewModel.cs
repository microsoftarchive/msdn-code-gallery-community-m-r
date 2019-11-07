namespace CIK.News.Web.Infras.ViewModels.Admin.DashBoard
{
    using System.Collections.Generic;

    using CIK.News.Entities.NewsAgg;

    public class DashBoardViewModel
    {
        public DashBoardViewModel()
        {
            this.Items = new List<Item>();
            this.PagingData = new PagingViewModel();
        }

        public string TitleSearchText { get; set; }

        public List<Item> Items { get; set; }

        public PagingViewModel PagingData { get; set; }
    }
}