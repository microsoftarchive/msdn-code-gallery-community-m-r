namespace CIK.News.Web.App_Start
{
    using AutoMapper;

    using CIK.News.Web.Infras.Profiles;

    public class MappingConfig
    {
        public static void Configure()
        {
            Mapper.AddProfile(new ItemMapperProfile());
            Mapper.AddProfile(new UserMapperProfile());
        }  
    }
}