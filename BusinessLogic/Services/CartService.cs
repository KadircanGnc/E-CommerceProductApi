using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BusinessLogic.Services
{
    public class CartService
    {
        private readonly CartRepository _cartRepo;
        private readonly ProductRepository _productRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int userId;

        public CartService(CartRepository cartRepo, ProductRepository productRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _cartRepo = cartRepo;
            _productRepo = productRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            userId = GetUserIdFromToken()!.Value;
        }

        //Converts user id to integer from token value
        private int? GetUserIdFromToken()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out var userId) ? userId : (int?)null;
        }

        public void AddItems(List<int> productIds)
        {                       

            if (userId <= 0)
            {
                throw new UnauthorizedAccessException("User ID could not be retrieved from the token.");
            }

            if (productIds == null || productIds.Count == 0)
            {
                throw new ArgumentException("Product IDs cannot be null or empty!");
            }

            // Fetch existing cart for the user
            var cart = _cartRepo.GetByUserId(userId);

            if (cart == null)
            {
                // Create a new cart if none exists
                cart = new Cart
                {
                    UserId = userId,
                    CreatedDate = DateTime.UtcNow,
                    CartItems = new List<CartItem>()
                };
            }
            else
            {
                // If cart exists, initialize CartItems if it's null
                if (cart.CartItems == null)
                {
                    cart.CartItems = new List<CartItem>();
                }
            }

            // Fetch products based on IDs
            var products = _productRepo.GetByIds(productIds);
            if (products == null || products.Count == 0)
            {
                throw new InvalidOperationException("No products found for the given IDs.");
            }

            // Add products to the cart
            foreach (var product in products)
            {
                // Check if there is enough stock
                if (product.StockCount <= 0)
                {
                    throw new InvalidOperationException($"Product '{product.Name}' is out of stock.");
                }

                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == product.Id);
                if (cartItem == null)
                {
                    cartItem = new CartItem
                    {
                        ProductId = product.Id,
                        Product = product,
                        Quantity = 1,
                        Price = product.Price
                    };
                    cart.CartItems.Add(cartItem);
                }
                else
                {
                    cartItem.Quantity++;
                    cartItem.Price = product.Price; // Update price
                }
            }

            // Recalculate the total amount of the cart
            cart.TotalPrice = cart.CartItems.Sum(ci => ci.Price * ci.Quantity);

            // Save the cart
            if (cart.Id <= 0)
            {
                _cartRepo.Create(cart);
            }
            else
            {
                _cartRepo.Update(cart);
            }
        }


        public void RemoveItems(List<int> productIds)
        {            
            if (productIds == null || productIds.Count == 0)
            {
                throw new ArgumentException("Product IDs cannot be null or empty.");
            }
            
            var cart = _cartRepo.GetByUserId(userId);
            if (cart == null)
            {
                throw new InvalidOperationException("Cart not found.");
            }            

            // Remove the products from the cart
            foreach (var productId in productIds)
            {
                var cartItem = cart.CartItems!.FirstOrDefault(ci => ci.ProductId == productId);
                if (cartItem != null)
                {
                    cart.CartItems!.Remove(cartItem);
                }
            }

            // Recalculate
            cart.TotalPrice = cart.CartItems!.Sum(ci => ci.Quantity * ci.Price);
            _cartRepo.Update(cart);
        }

        public void Clear()
        {
            var cartId = _cartRepo.GetByUserId(userId).Id;

            if (cartId <= 0)
            {
                throw new ArgumentException("Invalid cart ID.");
            }
            
            var cart = _cartRepo.GetById(cartId);
            if (cart == null)
            {
                throw new InvalidOperationException("Cart not found.");
            }
            
            cart.CartItems!.Clear();
            
            cart.TotalPrice = 0;
            
            _cartRepo.Update(cart);
        }

        public CartDTO Get()
        {
            var cartId = _cartRepo.GetByUserId(userId).Id;

            if (cartId <= 0)
            {
                throw new ArgumentException("Invalid cart ID.");
            }

            var cart = _cartRepo.GetById(cartId);
            if (cart == null)
            {
                throw new InvalidOperationException("Cart not found.");
            }
            
            var cartDto = _mapper.Map<CartDTO>(cart);

            return cartDto;
        }

        public CartDTO GetByUserId(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user ID.");
            }
            
            var cart = _cartRepo.GetByUserId(userId);
            if (cart == null)
            {
                throw new InvalidOperationException("Cart not found for the given user ID.");
            }
            
            var cartDto = _mapper.Map<CartDTO>(cart);

            return cartDto;
        }

    }
}
