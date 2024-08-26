using BusinessLogic.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTOs;
using FluentValidation;

namespace E_CommerceApi.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly IValidator<OrderDTO> _validator;
        public OrderController(OrderService orderService, IValidator<OrderDTO> validator)
        { 
               _orderService = orderService;
               _validator = validator;
        }

        [HttpGet]
        public List<OrderDTO> GetAllOrders()
        {
            var result = _orderService.GetOrders();
            if (result == null)
            {
                throw new ArgumentNullException("Invalid Value!");
            }
            return result;
        }

        [HttpPost]
        public IActionResult PlaceOrder([FromBody] int cartId)
        {
            if (cartId <= 0)
            {
                return BadRequest("Cart ID cannot be null or empty.");
            }

            try
            {
                _orderService.PlaceOrder(cartId);
                return Ok("Order created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }               

        [HttpDelete("/byOrder{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                _orderService.DeleteOrder(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/byOrder{id}")]
        public OrderDTO GetOrderById(int id)
        {
            var result = _orderService.GetOrderById(id);
            if (result == null)
            {
                throw new ArgumentNullException("Invalid Value");
            }
            return result;
        }
    }
}
