using System;
using System.Linq;
using DataAccessEfCore.DTOs;

namespace DataAccessEfCore.Repositories.Abstractions
{
    public interface IOrderRepo
    {
        ResultDTO_AddOrder AddOrder(OrderToAddDTO order);

        IQueryable<ProvinceCheckoutDTO> GetProvinces();

        IQueryable<OrderDTO> GetOrdersByUserId(int userId);

        OrderDetailDTO GetOrderById(Guid customerOrderId);

        OrderDetailDTO GetOrderByIdEmail(Guid customerOrderId, string email);
    }
}
