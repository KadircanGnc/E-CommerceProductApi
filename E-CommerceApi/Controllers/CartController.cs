using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApi.Controllers
{
    [Route("carts")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;
        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("addItems")]
        public IActionResult AddItemsToCart(int userId,[FromBody] List<int> productIds)
        {
            if (userId <= 0 || productIds.Count <= 0)
            {
                return BadRequest("Product IDs cannot be null or empty.");
            }

            try
            {
                _cartService.AddItemToCart(userId, productIds);
                return Ok("Items added to cart successfully.");
            }            
            catch (Exception ex)
            {                
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("removeItems")]
        public IActionResult RemoveItemsFromCart(int cartId, [FromBody] List<int> productIds)
        {
            if (cartId <= 0 || productIds.Count <= 0)
            {
                return BadRequest("Product IDs cannot be null or empty.");
            }

            try
            {
                _cartService.RemoveItemFromCart(cartId, productIds);
                return Ok("Items removed from cart successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("clear{cartId}")]
        public IActionResult ClearCart(int cartId)
        {
            if (cartId <= 0)
            {
                return BadRequest("Invalid cart ID.");
            }

            try
            {
                _cartService.ClearCart(cartId);
                return Ok("Cart cleared successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpGet("{cartId}")]
        public IActionResult GetCart(int cartId)
        {
            if (cartId <= 0)
            {
                return BadRequest("Invalid cart ID.");
            }

            try
            {
                var cart = _cartService.GetCartById(cartId);
                if (cart == null)
                {
                    return NotFound("Cart not found.");
                }

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpGet("user/{userId}")]
        public ActionResult<CartDTO> GetCartByUserId(int userId)
        {
            try
            {
                var cart = _cartService.GetCartByUserId(userId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }            
        }
    }
}
