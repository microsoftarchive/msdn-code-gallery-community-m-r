namespace CIK.News.Entities.NewsAgg
{
    using System.Collections.Generic;

    public class Category : Entity
    {
        public Category()
        {
            this.Items = new List<Item>();
        }

        public virtual string Name
        {
            get;
            set;
        }

        public virtual IList<Item> Items { get; set; }
    }
}
