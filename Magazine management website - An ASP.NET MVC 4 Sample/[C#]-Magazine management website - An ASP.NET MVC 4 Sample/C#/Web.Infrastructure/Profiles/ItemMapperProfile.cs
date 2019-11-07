namespace CIK.News.Web.Infras.Profiles
{
    using AutoMapper;
    
    using CIK.News.Entities.NewsAgg;
    using CIK.News.Web.Infras.ViewModels.Admin.DashBoard;

    public class ItemMapperProfile : Profile 
    {
        protected override void Configure()
        {
            //Mapper.CreateMap<ItemCreatingViewModel, Item>()
            //    .ForMember(x => x.Category, o => o.MapFrom(m => new Category { Id = m.CategoryId }))
            //    .ForMember(x => x.ItemContent, o => o.MapFrom(m => new ItemContent { Title = m.Title }))
            //    .ForMember(x => x.ItemContent, o => o.MapFrom(m => new ItemContent { SortDescription = m.SortDescription }))
            //    .ForMember(x => x.ItemContent, o => o.MapFrom(m => new ItemContent { Content = m.Content }))
            //    .ForMember(x => x.ItemContent, o => o.MapFrom(m => new ItemContent { SmallImage = m.SmallImagePath }))
            //    .ForMember(x => x.ItemContent, o => o.MapFrom(m => new ItemContent { MediumImage = m.MediumImagePath }))
            //    .ForMember(x => x.ItemContent, o => o.MapFrom(m => new ItemContent { BigImage = m.BigImagePath }));

            Mapper.CreateMap<Item, ItemEditingViewModel>()
                .ForMember(x => x.ItemId, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.CategoryId, o => o.MapFrom(m => m.Category.Id))
                .ForMember(x => x.Title, o => o.MapFrom(m => m.ItemContent.Title))
                .ForMember(x => x.SortDescription, o => o.MapFrom(m => m.ItemContent.SortDescription))
                .ForMember(x => x.Content, o => o.MapFrom(m => m.ItemContent.Content))
                .ForMember(x => x.SmallImagePath, o => o.MapFrom(m => m.ItemContent.SmallImage))
                .ForMember(x => x.MediumImagePath, o => o.MapFrom(m => m.ItemContent.MediumImage))
                .ForMember(x => x.BigImagePath, o => o.MapFrom(m => m.ItemContent.BigImage));
        }
    }
}