using OwinSignalR.Common.Dto;
using OwinSignalR.Data.Models;

using AutoMapper;

namespace OwinSignalR.Data.Configuration
{
    public static class AutomapperConfiguration
    {
        public static void Configure() 
        { 
            Mapper.CreateMap<Application, ApplicationDto>()
                .ForMember(dest => dest.ApplicationId          , opt => opt.MapFrom(src => src.ApplicationId))
                .ForMember(dest => dest.ApplicationName        , opt => opt.MapFrom(src => src.ApplicationName))
                .ForMember(dest => dest.ApiToken               , opt => opt.MapFrom(src => src.ApiToken))
                .ForMember(dest => dest.ApplicationSecret      , opt => opt.MapFrom(src => src.ApplicationSecret))
                .ForMember(dest => dest.ApplicationReferralUrls, opt => opt.MapFrom(src => src.ApplicationReferralUrls));

            Mapper.CreateMap<ApplicationReferralUrl, ApplicationReferralUrlDto>()
                .ForMember(dest => dest.ApplicationReferralUrlId, opt => opt.MapFrom(src => src.ApplicationReferralUrlId))
                .ForMember(dest => dest.ApplicationId           , opt => opt.MapFrom(src => src.ApplicationId))
                .ForMember(dest => dest.Url                     , opt => opt.MapFrom(src => src.Url));
        }
    }
}
