namespace CIK.News.Web.Infras.ViewModels.Admin.Persistences
{
    using System;
    using System.Data;
    using System.Web.Mvc;

    using CIK.News.Entities;
    using CIK.News.Entities.NewsAgg;
    using CIK.News.Entities.UserAgg;
    using CIK.News.Framework.Extensions;
    using CIK.News.Web.Infras.Repository;

    public class ItemCreatingPersistence : IItemCreatingPersistence
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public ItemCreatingPersistence()
            : this(DependencyResolver.Current.GetService<IItemRepository>(),
                    DependencyResolver.Current.GetService<ICategoryRepository>(),
                    DependencyResolver.Current.GetService<IUserRepository>())
        {
        }

        public ItemCreatingPersistence(IItemRepository itemRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            this._itemRepository = itemRepository;
            this._categoryRepository = categoryRepository;
            this._userRepository = userRepository;
        }

        public bool PersistenceItem(Item item)
        {
            var category = this._categoryRepository.GetCategoryById(item.Category.Id);

            if(category == null)
                throw new NoNullAllowedException("Category".ToNotNullErrorMessage());

            item.Category = category;

            item.CreatedDate = DateTime.Now;

            item.ItemContent.CreatedDate = DateTime.Now;

            var user = this._userRepository.GetUserByUserName(item.CreatedBy);

            if(user == null)
                throw new NoNullAllowedException("You have to login to system!!!");

            item.CreatedBy = user.UserName;
            item.ItemContent.CreatedBy = user.UserName;

            return this._itemRepository.SaveItem(item);
        }
    }
}