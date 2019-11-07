namespace CIK.News.Web.Infras.ViewModels.Admin.Persistences
{
    using System;
    using System.Data;
    using System.Web.Mvc;

    using CIK.News.Entities;
    using CIK.News.Entities.NewsAgg;
    using CIK.News.Framework.Extensions;
    using CIK.News.Web.Infras.Repository;

    public class ItemEditingPersistence : IItemEditingPersistence
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ItemEditingPersistence()
            : this(DependencyResolver.Current.GetService<IItemRepository>(),
                    DependencyResolver.Current.GetService<ICategoryRepository>())
        {
        }

        public ItemEditingPersistence(IItemRepository itemRepository, ICategoryRepository categoryRepository)
        {
            this._itemRepository = itemRepository;
            this._categoryRepository = categoryRepository;
        }

        public bool PersistenceItem(Item item)
        {
            var category = this._categoryRepository.GetCategoryById(item.Category.Id);

            if (category == null)
                throw new NoNullAllowedException("Category".ToNotNullErrorMessage());

            var oldItem = this._itemRepository.GetById(item.Id);

            if (oldItem == null && oldItem.ItemContent == null)
                throw new NoNullAllowedException(string.Format("Item with id={0}", item.Id).ToNotNullErrorMessage());

            if(category.Id != oldItem.Category.Id)
                oldItem.Category = category;

            oldItem.ModifiedDate = DateTime.Now;

            oldItem.ItemContent.ModifiedDate = DateTime.Now;

            // mapping information
            var oldItemContent = oldItem.ItemContent;
            var itemContent = item.ItemContent;

            if (!oldItemContent.Title.Equals(itemContent.Title, StringComparison.InvariantCulture))
                oldItem.ItemContent.Title = itemContent.Title;

            if (!oldItemContent.SortDescription.Equals(itemContent.SortDescription, StringComparison.InvariantCulture))
                oldItem.ItemContent.SortDescription = itemContent.SortDescription;

            if (!oldItemContent.Content.Equals(itemContent.Content, StringComparison.InvariantCulture))
                oldItem.ItemContent.Content = itemContent.Content;

            if (itemContent.SmallImage != null && !oldItemContent.SmallImage.Equals(itemContent.SmallImage, StringComparison.InvariantCulture))
                oldItem.ItemContent.SmallImage = itemContent.SmallImage;

            if (itemContent.MediumImage != null && !oldItemContent.MediumImage.Equals(itemContent.MediumImage, StringComparison.InvariantCulture))
                oldItem.ItemContent.MediumImage = itemContent.MediumImage;

            if (itemContent.BigImage != null && !oldItemContent.BigImage.Equals(itemContent.BigImage, StringComparison.InvariantCulture))
                oldItem.ItemContent.BigImage = itemContent.BigImage;

            return this._itemRepository.SaveItem(oldItem);
        }
    }
}