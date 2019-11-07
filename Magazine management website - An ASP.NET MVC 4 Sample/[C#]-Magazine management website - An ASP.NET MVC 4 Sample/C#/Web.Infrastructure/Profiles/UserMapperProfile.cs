namespace CIK.News.Web.Infras.Profiles
{
    using AutoMapper;
    
    using CIK.News.Entities.UserAgg;
    using CIK.News.Web.Infras.Security;

    public class UserMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, UserInfo>()
                .ForMember(x => x.UserId, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.GroupId, o => o.MapFrom(m => m.Role));
        }
    }
}