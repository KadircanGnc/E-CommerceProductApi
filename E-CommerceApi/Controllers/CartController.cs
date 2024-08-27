using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApi.Controllers
{
    [Route("[Controller]s")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;
        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize(Roles = "admin,user")]
        [HttpPost("add-items")]
        public IActionResult AddItems([FromBody] List<int> productIds)
        {
            if (productIds.Count <= 0)
            {
                return BadRequest("Product IDs cannot be null or empty.");
            }

            _cartService.AddItems(productIds);
            return Ok("Items added to cart successfully.");
        }

        [Authorize(Roles = "admin,user")]
        [HttpPost("remove-items")]
        public IActionResult RemoveItems([FromBody] List<int> productIds)
        {
            if (productIds.Count <= 0)
            {
                return BadRequest("Product IDs cannot be null or empty.");
            }

            _cartService.RemoveItems(productIds);
            return Ok("Items removed from cart successfully.");
        }

        [Authorize(Roles = "admin,user")]
        [HttpPost("clear{cartId}")]
        public IActionResult Clear()
        {           
            _cartService.Clear();
            return Ok("Cart cleared successfully.");
        }

        [Authorize(Roles = "admin,user")]
        [HttpGet]
        public IActionResult Get()
        {
            var cart = _cartService.Get();
            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            return Ok(cart);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("get-by-user-id{userId}")]
        public IActionResult GetByUserId(int userId)
        {
            var cart = _cartService.GetByUserId(userId);
            return Ok(cart);
        }
    }
}
