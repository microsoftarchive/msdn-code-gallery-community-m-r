namespace CIK.News.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    using CIK.News.Entities.NewsAgg;
    using CIK.News.Web.Infras;
    using CIK.News.Web.Infras.ActionResults.Admin;
    using CIK.News.Web.Infras.MediaItem;
    using CIK.News.Web.Infras.ViewModels.Admin.DashBoard;
    using CIK.News.Web.Infras.ViewModels.Admin.Persistences;

    [Authorize]
    public class ItemController : BaseController
    {
        #region ctors & variables
        
        private readonly IItemCreatingPersistence _itemCreatingPersistence;
        private readonly IItemEditingPersistence _itemEditingPersistence;
        private readonly IItemDeletingPersistence _itemDeletingPersistence;
        private readonly IMediaItemStorage _itemStorage;

        public ItemController(
            IItemCreatingPersistence itemCreatingPersistence, 
            IItemDeletingPersistence itemDeletingPersistence,
            IItemEditingPersistence itemEditingPersistence,
            IMediaItemStorage itemStorage)
        {
            _itemCreatingPersistence = itemCreatingPersistence;
            _itemDeletingPersistence = itemDeletingPersistence;
            _itemEditingPersistence = itemEditingPersistence;

            _itemStorage = itemStorage;
        }

        #endregion

        #region public methods

        public ActionResult Create()
        {
            return new ItemCreatingViewModelActionResult<ItemController>(x => x.Create());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ItemCreatingViewModel viewModel)
        {
            var item = CreateOrUpdateItem(viewModel, true);

            if (_itemCreatingPersistence.PersistenceItem(item))
                SucceedMessage("Save item successfully");
            else
                ErrorMessage("Cannot create item");

            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult Edit(int id)
        {
            return new ItemEditingViewModelActionResult<ItemController>(x => x.Edit(id), id);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ItemEditingViewModel viewModel)
        {
            var item = CreateOrUpdateItem(viewModel, false);

            if (_itemEditingPersistence.PersistenceItem(item))
                SucceedMessage("Save item successfully");
            else
                ErrorMessage("Cannot edit item");

            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult Delete(int id)
        {
            var isSucceed = _itemDeletingPersistence.PersistenceItem(id);

            if(isSucceed)
                SucceedMessage("Delete item successfully");
            else
                ErrorMessage("Cannot delete item");

            return RedirectToAction("Index", "Dashboard");
        }

        #endregion

        #region private methods

        private Item CreateOrUpdateItem(dynamic vm, bool isNew)
        {
            var smallImagePath = string.Empty;
            var mediumImagePath = string.Empty;
            var largeImagePath = string.Empty;

            if (vm.SmallImage != null)
            {
                smallImagePath = vm.SmallImage.CreateImagePathFromStream(_itemStorage);
            }

            if (vm.MediumImage != null)
            {
                mediumImagePath = vm.MediumImage.CreateImagePathFromStream(_itemStorage);
            }

            if (vm.BigImage != null)
            {
                largeImagePath = vm.BigImage.CreateImagePathFromStream(_itemStorage);
            }

            var category = CategoryFactory.CreateCategory(vm.CategoryId);
            var itemContent = ItemContentFactory.CreateItemContent(
                                    vm.Title,
                                    vm.SortDescription,
                                    vm.Content,
                                    smallImagePath,
                                    mediumImagePath,
                                    largeImagePath);

            Item item = isNew
                        ? ItemFactory.CreateItem(this.GetUserName(), category, itemContent)
                        : ItemFactory.CreateItem(vm.ItemId, this.GetUserName(), category, itemContent);

            return item;
        }

        #endregion
    }
}
