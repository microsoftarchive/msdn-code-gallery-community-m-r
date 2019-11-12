namespace CIK.News.Web.Infras.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using CIK.News.Data;
    using CIK.News.Entities.NewsAgg;

    public class CategoryRepository : GenericRepository, ICategoryRepository
    {
        public CategoryRepository()
            : this(DependencyResolver.Current.GetService<MyDbContext>())
        {
        }

        public CategoryRepository(MyDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Category> GetCategories()
        {
            return this.GetQuery<Category>().ToList();
        }

        public Category GetCategoryById(int id)
        {
            return this.FindOne<Category>(x => x.Id == id);
        }
    }
}