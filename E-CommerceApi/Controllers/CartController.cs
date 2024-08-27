using BusinessLogic.DTOs;
using BusinessLogic.Services;
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

        [HttpPost("add-items")]
        public IActionResult AddItems(int userId,[FromBody] List<int> productIds)
        {
            if (userId <= 0 || productIds.Count <= 0)
            {
                return BadRequest("Product IDs cannot be null or empty.");
            }

            _cartService.AddItems(userId, productIds);
            return Ok("Items added to cart successfully.");
        }

        [HttpPost("remove-items")]
        public IActionResult RemoveItems(int cartId, [FromBody] List<int> productIds)
        {
            if (cartId <= 0 || productIds.Count <= 0)
            {
                return BadRequest("Product IDs cannot be null or empty.");
            }

            _cartService.RemoveItems(cartId, productIds);
            return Ok("Items removed from cart successfully.");
        }

        [HttpPost("clear{cartId}")]
        public IActionResult Clear(int cartId)
        {
            if (cartId <= 0)
            {
                return BadRequest("Invalid cart ID.");
            }

            _cartService.Clear(cartId);
            return Ok("Cart cleared successfully.");
        }

        [HttpGet]
        public IActionResult GetById(int cartId)
        {
            var cart = _cartService.GetById(cartId);
            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            return Ok(cart);
        }

        [HttpGet("get-by-user-id{userId}")]
        public IActionResult GetByUserId(int userId)
        {
            var cart = _cartService.GetByUserId(userId);
            return Ok(cart);
        }
    }
}
