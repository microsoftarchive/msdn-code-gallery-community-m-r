namespace CIK.News.Web.Infras.ViewModels.Admin.Persistences
{
    using System.Data;

    using CIK.News.Entities.NewsAgg;
    using CIK.News.Framework.Extensions;
    using CIK.News.Web.Infras.Repository;

    public class ItemDeletingPersistence : IItemDeletingPersistence
    {
        private readonly IItemRepository _itemRepository;

        public ItemDeletingPersistence(IItemRepository itemRepository)
        {
            this._itemRepository = itemRepository;
        }

        public bool PersistenceItem(int id)
        {
            var item = this._itemRepository.GetById(id);

            if(item == null)
                throw new NoNullAllowedException(string.Format("Item with id={0}", id).ToNotNullErrorMessage());

            return this._itemRepository.DeleteItem(item);
        }
    }
}