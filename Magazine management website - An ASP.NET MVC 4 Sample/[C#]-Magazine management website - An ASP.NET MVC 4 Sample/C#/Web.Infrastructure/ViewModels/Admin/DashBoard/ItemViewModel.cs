namespace CIK.News.Web.Infras.ViewModels.Admin.DashBoard
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CIK.News.Entities;
    using CIK.News.Entities.NewsAgg;

    public class ItemViewModel
    {
        public ItemViewModel()
        {
            this.Categories = new List<Category>();
        }

        [Required]
        public int CategoryId { get; set; }

        public List<Category> Categories { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string SortDescription { get; set; }

        [Required]
        public string Content { get; set; }
    }
}