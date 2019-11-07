namespace CIK.News.Web.Infras.ViewModels.Admin.Persistences
{
    using CIK.News.Entities.NewsAgg;

    public interface IItemCreatingPersistence
    {
        bool PersistenceItem(Item item);
    }
}