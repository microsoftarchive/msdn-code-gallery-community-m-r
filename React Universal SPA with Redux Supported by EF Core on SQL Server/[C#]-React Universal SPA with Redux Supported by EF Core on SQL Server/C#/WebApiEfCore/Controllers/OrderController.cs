using System;
using System.Linq;
using DataAccessEfCore.DTOs;
using DataAccessEfCore.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiEfCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController: ControllerBase
    {
        private readonly IOrderRepo _orderRepo;

        public OrderController(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        // POST api/Order/addOrder
        [HttpPost("addOrder")]
        public ActionResult<ResultDTO_AddOrder>  AddOrder(OrderToAddDTO order)
        {
            try
            {
                var result = _orderRepo.AddOrder(order);

                return result;
            }
            catch (Exception e)
            {
                // TODO log(e)
                Console.WriteLine(e);

                return new BadRequestResult(); 
            }
        }

        // GET api/Order/getProvinces
        [HttpGet("getProvinces")]
        public ActionResult<IQueryable<ProvinceCheckoutDTO>> GetProvinces()
        {
            var provinces = _orderRepo.GetProvinces();

            return Ok(provinces);
        }

        // GET api/Order/getOrdersByUserId
        [HttpGet("getOrdersByUserId")]
        [Authorize]
        public ActionResult<IQueryable<OrderDTO>> GetOrdersByUserId(int userId)
        {
            var result = _orderRepo.GetOrdersByUserId(userId);

            return Ok(result);
        }

        // GET api/Order/getOrderDetailById
        [HttpGet("getOrderDetailById")]
        [Authorize]
        public ActionResult<OrderDetailDTO> GetOrderDetailById(Guid customerOrderId)
        {
            var result = _orderRepo.GetOrderById(customerOrderId);

            return Ok(result);
        }

        [HttpGet("getOrderDetailByIdEmail")]
        public ActionResult<OrderDetailDTO> GetOrderDetailByIdEmail(Guid customerOrderId, string email)
        {
            var result = _orderRepo.GetOrderByIdEmail(customerOrderId, email);

            return Ok(result);
        }
    }
}
