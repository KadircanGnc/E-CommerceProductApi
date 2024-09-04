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
        private readonly ICartService _cartService;
        public OrderController(IOrderService orderService, IPaymentService paymentService, ICartService cartService)
        {
            _orderService = orderService;   
            _paymentService = paymentService;
            _cartService = cartService;
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
        public IActionResult Create([FromBody] CreditCardDTO creditCardDTO)
        {
            var userId = _cartService.GetUserId();
            var cartId = _cartService.GetCartId();
            if (cartId <= 0 && _paymentService.IsPayCompleted(creditCardDTO).Result == false)
            {
                return BadRequest("You need to complete payment first.");
            }

            _orderService.Create(cartId);
            _cartService.Clear();
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
