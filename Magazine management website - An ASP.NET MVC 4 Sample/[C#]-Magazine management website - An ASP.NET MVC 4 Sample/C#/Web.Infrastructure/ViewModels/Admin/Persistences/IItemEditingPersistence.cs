namespace CIK.News.Web.Infras.ViewModels.Admin.Persistences
{
    using CIK.News.Entities;
    using CIK.News.Entities.NewsAgg;

    public interface IItemEditingPersistence
    {
        bool PersistenceItem(Item item);
    }
}