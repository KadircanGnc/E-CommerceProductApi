using BusinessLogic.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTOs;
using FluentValidation;

namespace E_CommerceApi.Controllers
{
    [Route("[Controller]s")]
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
        public IActionResult GetAll()
        {
            var result = _orderService.GetAll();
            if (result == null)
            {
                return BadRequest("Invalid Value!");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] int cartId)
        {
            if (cartId <= 0)
            {
                return BadRequest("Cart ID cannot be null or empty.");
            }

            _orderService.Create(cartId);
            return Ok("Order created successfully.");
        }               

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _orderService.Delete(id);
            return Ok();
        }

        [HttpGet("by-id{id}")]
        public IActionResult GetById(int id)
        {
            var result = _orderService.GetById(id);
            if (result == null)
            {
                return BadRequest("Invalid Value");
            }
            return Ok(result);
        }
    }
}
