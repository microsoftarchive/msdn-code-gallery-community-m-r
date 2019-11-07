using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccessEfCore.DataAccess;
using DataAccessEfCore.DbModels;
using DataAccessEfCore.DTOs;
using DataAccessEfCore.Repositories.Abstractions;

namespace DataAccessEfCore.Repositories
{
    public class OrderRepo: GeneralRepo<Order>, IOrderRepo
    {
        public OrderRepo(SkiShopDbContext dbContext, IConfigurationProvider mapperProvider, IMapper mapper)
            : base(dbContext, mapperProvider, mapper) { }

        public ResultDTO_AddOrder AddOrder(OrderToAddDTO order) 
        {
            var result = new ResultDTO_AddOrder
            {
                SkuIdsOverStock = order.OrderItems.Join(_DbContext.Skus, oi => oi.SkuId, sk => sk.SkuId,
                        (oi, sk) => new
                        {
                            skuId = oi.SkuId, quantityToOrder = oi.Quantity, quantityInStock = sk.Quantity
                        })
                    .Where(z => z.quantityInStock < z.quantityToOrder)
                    .Select(z => z.skuId)
            };

            var styleIds = order.OrderItems.Join(_DbContext.Skus, oi => oi.SkuId, sk => sk.SkuId,
                (oi, sk) => new
                {
                    styleId = sk.StyleId
                }).Distinct().ToList();

            var skus = styleIds.Join(_DbContext.Skus, st => st.styleId, sk => sk.StyleId,
                (st, sk) => new SkuStyleDTO
                {
                    SkuId = sk.SkuId,
                    StyleId = st.styleId,
                    Size = sk.Size,
                    Quantity = sk.Quantity
                }).ToList();

            var styleStates = styleIds.Join(_DbContext.StyleStates, st => st.styleId, ss => ss.StyleId,
                (st, ss) => new StyleState
                {
                    StyleId = st.styleId,
                    AverageRatings = ss.AverageRatings,
                    ReviewCount = ss.ReviewCount,
                    SoldOut = ss.SoldOut
                }).ToList();

            if (result.SkuIdsOverStock.Any())
            {
                result.OrderId = -1;
                result.Skus = skus;
                result.StyleStates = styleStates;

                return result;
            }

            var orderToAdd = _mapper.Map<Order>(order);

            SaveChange(orderToAdd);

            result.OrderId = orderToAdd.OrderId;
            result.CustomerOrderId = orderToAdd.CustomerOrderId;

            result.Skus = skus.GroupJoin(order.OrderItems, sk => sk.SkuId, oi => oi.SkuId,
                (sk, ois) =>
                {
                    var orderItems = ois.ToList();
                    return new SkuStyleDTO
                    {
                        SkuId = sk.SkuId,
                        StyleId = sk.StyleId,
                        Size = sk.Size,
                        Quantity = orderItems.Any()
                            ? (short)(sk.Quantity - orderItems.First().Quantity)
                            : sk.Quantity
                    };
                }).ToList();

            result.StyleStates = styleStates.Select(ss =>
            {
                var sum = result.Skus.Where(sku => sku.StyleId == ss.StyleId).ToList().Sum(sku => sku.Quantity);

                return new StyleState
                {
                    StyleId = ss.StyleId,
                    AverageRatings = ss.AverageRatings,
                    ReviewCount = ss.ReviewCount,
                    SoldOut = sum ==0
                };
            });

            return result;
        }

        public IQueryable<ProvinceCheckoutDTO> GetProvinces()
        {
            var provinces = _DbContext.Provinces.ProjectTo<ProvinceCheckoutDTO>(_mapperProvider);

            return provinces;
        }

        public IQueryable<OrderDTO> GetOrdersByUserId(int userId)
        {
            var orders = GetAll(order => order.UserId == userId)
                .OrderByDescending(order => order.CreatedDateTime)
                .ProjectTo<OrderDTO>(_mapperProvider);

            return orders;
        }

        public OrderDetailDTO GetOrderById(Guid customerOrderId)
        {
            var order = GetAll().Where(o => o.CustomerOrderId == customerOrderId)
                .ProjectTo<OrderDetailDTO>(_mapperProvider).FirstOrDefault();

            return order;
        }

        public OrderDetailDTO GetOrderByIdEmail(Guid customerOrderId, string email)
        {
            var order = GetAll().Where(o => o.CustomerOrderId == customerOrderId && o.Email == email)
                .ProjectTo<OrderDetailDTO>(_mapperProvider).FirstOrDefault();

            return order;
        }

    }
}
