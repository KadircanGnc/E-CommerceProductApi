using Common.DTOs;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApi.Controllers
{
    [Route("[Controller]s")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        [HttpPost("clear")]
        public IActionResult Clear()
        {           
            _cartService.Clear();
            return Ok("Cart cleared successfully.");
        }

        [Authorize]
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

		[Authorize]
		[HttpGet("get-cart-items")]
		public IActionResult GetCartItems()
		{
			var cartItems = _cartService.GetCartItems();
			if (cartItems == null)
			{
				return NotFound("No Cart Items found.");
			}

			return Ok(cartItems);
		}

		[Authorize(Roles = "admin")]
        [HttpGet("get-by-user-id")]
        public IActionResult GetByUserId(int userId)
        {
            var cart = _cartService.GetByUserId(userId);
            return Ok(cart);
        }

		[Authorize]
		[HttpGet("get-item-count")]
		public IActionResult GetItemCount()
		{
			var result = _cartService.GetItemCount();
			if (result <= 0)
			{
				return NotFound("No items found.");
			}

			return Ok(result);
		}
	}
}
