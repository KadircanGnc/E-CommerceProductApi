using BusinessLogic.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTOs;

namespace E_CommerceApi.Controllers
{
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        { 
               _orderService = orderService;
        }

        [HttpGet]
        public List<GetOrderDTO> GetAllOrders()
        {
            var result = _orderService.GetOrders();
            if (result == null)
            {
                throw new ArgumentNullException("Invalid Value!");
            }
            return result;
        }

        [HttpPost]
     /*   public IActionResult CreateOrder([FromBody] List<int> productIds)
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
        } */

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id,[FromBody] OrderDTO order)
        {
            if (id <= 0)
                return BadRequest("Invalid ID!");
            try
            {
                _orderService.UpdateOrder(order);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
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

        [HttpGet("{id}")]
        public GetOrderDTO GetOrderById(int id)
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
