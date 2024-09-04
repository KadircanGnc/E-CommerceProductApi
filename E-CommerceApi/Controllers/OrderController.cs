using BusinessLogic.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace E_CommerceApi.Controllers
{
    [Route("[Controller]s")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        public OrderController(IOrderService orderService, IPaymentService paymentService)
        {
            _orderService = orderService;   
            _paymentService = paymentService;
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        public IActionResult Create([FromBody] int cartId)
        {
            if (cartId <= 0 && _paymentService.Pay() == false)
            {
                return BadRequest("You need to complete payment first.");
            }

            _orderService.Create(cartId);
            return Ok("Order created successfully.");
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            _orderService.Delete(id);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("by-id")]
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
