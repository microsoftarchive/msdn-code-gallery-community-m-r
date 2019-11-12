using System;
using AutoMapper;
using DataAccessEfCore.DbModels;
using DataAccessEfCore.DTOs;

namespace DataAccessEfCore.Utilities
{
    public class AutoMapperConfiguration: Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Style, SkisDTO>()
                .ForMember(dest => dest.StyleExtra, options => options.MapFrom(src => src))
                .ForMember(desc => desc.State, options => options.MapFrom(src => src.StyleState))
                .ForMember(dest => dest.Skus, options => options.MapFrom(src => src.Skus))
                .ForMember(dest => dest.Descs, options => options.MapFrom(src => src.Descriptions));

            CreateMap<UserIdentity, UserDTO>()
                .ForMember(dest => dest.FullName, options => options.MapFrom(
                    src => src.FirstName + " " + src.LastName));

            CreateMap<Review, ReviewDTO>()
                .ForMember(dest => dest.ScreenName, options => options.MapFrom(src => src.User.ScreenName))
                .ForMember(dest => dest.CreatedDateTime, options => options.MapFrom(src =>
                    src.CreatedDateTime.ToString("MMM d yyyy h:mm tt")));

            CreateMap<OrderToAddDTO, Order>()
                .ForMember(dest => dest.CustomerOrderId, options => options.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FullName, options => options.MapFrom(src => src.FullName))
                .ForMember(dest => dest.ProvinceId, options => options.MapFrom(src => src.ProvinceId))
                .ForMember(dest => dest.City, options => options.MapFrom(src => src.City))
                .ForMember(dest => dest.AddressLine, options => options.MapFrom(src => src.AddressLine))
                .ForMember(dest => dest.PostalCode, options => options.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.TotalValue, options => options.MapFrom(src => src.TotalValue))
                .ForMember(dest => dest.CreatedDateTime, options => options.MapFrom(src => src.CreatedDateTime))
                .ForMember(dest => dest.OrderItems, options => options.MapFrom(src => src.OrderItems));

            CreateMap<OrderItemToAddDTO, OrderItem>()
                .ForMember(dest => dest.SkuId, options => options.MapFrom(src => src.SkuId))
                .ForMember(dest => dest.Price, options => options.MapFrom(src => src.Price))
                .ForMember(dest => dest.Quantity, options => options.MapFrom(src => src.Quantity));

            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.CreatedDateTime, options => options.MapFrom(
                    src => src.CreatedDateTime.ToString("MMM d yyyy h:mm tt")));

            CreateMap<Order, OrderDetailDTO>()
                .ForMember(dest => dest.ProvinceName, options => options.MapFrom(src => src.Province.ProvinceName))
                .ForMember(dest => dest.CreatedDateTime, options => options.MapFrom(src => src.CreatedDateTime))
                .ForMember(dest => dest.OrderItems, options => options.MapFrom(src => src.OrderItems));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.SkuId, options => options.MapFrom(src => src.SkuId))
                .ForMember(dest => dest.Quantity, options => options.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Skis, options => options.MapFrom(src =>
                    src.Sku.Style.Brand.BrandName + "-" + src.Sku.Style.StyleName + '-'
                    + src.Sku.Style.Gender.GenderName + "-" + src.Sku.Size));


        }
    }
}
