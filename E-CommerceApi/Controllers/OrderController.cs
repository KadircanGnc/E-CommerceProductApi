using BusinessLogic.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTOs;
using FluentValidation;

namespace E_CommerceApi.Controllers
{
    [Route("api/order")]
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

        [HttpGet("AllOrders")]
        public List<OrderDTO> GetAllOrders()
        {
            var result = _orderService.GetOrders();
            if (result == null)
            {
                throw new ArgumentNullException("Invalid Value!");
            }
            return result;
        }

        [HttpPost("Order")]
        public IActionResult CreateOrder([FromBody] List<int> productIds)
        {
            if (productIds == null || productIds.Count == 0)
            {
                return BadRequest("Product IDs cannot be null or empty.");
            }

            try
            {
                _orderService.CreateOrderWithProducts(productIds);
                return Ok("Order created and products added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 

        [HttpPut("/AddProductToOrder")]
        public IActionResult AddProductToOrder(int orderId,[FromBody] List<int> productIds)
        {
            if (orderId <= 0)
                return BadRequest("Invalid ID!");
            try
            {
                _orderService.AddProductsToOrder(orderId, productIds);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/RemoveProductFromOrder")]
        public IActionResult RemoveProductFromOrder(int orderId, [FromBody] List<int> productIds)
        {
            if (orderId <= 0)
                return BadRequest("Invalid ID!");
            try
            {
                _orderService.RemoveProductsFromOrder(orderId, productIds);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/ByOrder{id}")]
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

        [HttpGet("/ByOrder{id}")]
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
