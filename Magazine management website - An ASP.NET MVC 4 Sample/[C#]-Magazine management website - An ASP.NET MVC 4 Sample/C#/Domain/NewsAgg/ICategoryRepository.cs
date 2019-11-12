namespace CIK.News.Entities.NewsAgg
{
    using System.Collections.Generic;

    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();

        Category GetCategoryById(int id);
    }
}