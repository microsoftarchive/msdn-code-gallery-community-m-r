namespace CIK.News.Web.Infras.ViewModels.Admin.Persistences
{
    public interface IItemDeletingPersistence
    {
        bool PersistenceItem(int id);
    }
}