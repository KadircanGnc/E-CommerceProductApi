using System;
using System.Collections.Generic;
using AutoMapper;
using DataAccess.Interfaces;
using BusinessLogic.DTOs;
using Entities;
using BusinessLogic.Interfaces;

namespace BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderProductRepository _opRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IProductRepository productRepository, IOrderProductRepository op, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;         
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _opRepository = op;
            _mapper = mapper;
        }

        public void Create(int cartId)
        {
            if (cartId <= 0)
            {
                throw new ArgumentException("Invalid cart ID.");
            }

            // Fetch the cart
            var cart = _cartRepository.GetById(cartId);
            if (cart == null)
            {
                throw new InvalidOperationException("Cart not found.");
            }

            // Create a new order
            var order = new Order
            {
                CreatedDate = DateTime.UtcNow,
                UserId = cart.UserId,
                TotalAmount = cart.TotalPrice // Assuming cart has the total price calculated
            };

            _orderRepository.Create(order);

            // Add cart items to the order through OrderProduct
            foreach (var cartItem in cart.CartItems!)
            {
                // Check if there is enough stock
                var product = _productRepository.GetById(cartItem.ProductId);
                if (product == null || product.StockCount < cartItem.Quantity)
                {
                    throw new InvalidOperationException($"Product '{product?.Name}' is out of stock.");
                }

                // Decrease the stock count
                product.StockCount -= cartItem.Quantity;
                _productRepository.Update(product);

                var orderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId                    
                };

                _opRepository.Create(orderProduct);
            }

            // Clear the cart
            cart.CartItems.Clear();
            cart.TotalPrice = 0;
            _cartRepository.Update(cart);
        }              


        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("Value is Invalid!");
            }
            _orderRepository.Delete(id);
        }

        public OrderDTO GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("Invalid ID!");
            }

            var result = _orderRepository.GetById(id);
            if (result == null)
            {
                throw new ArgumentNullException("Value is Null!");
            }
            
            return _mapper.Map<OrderDTO>(result);
        }

        public List<OrderDTO> GetAll()
        {
            var result = _orderRepository.GetAll();
            if (result == null)
            {
                throw new ArgumentNullException("Value is Null!");
            }
            
            return _mapper.Map<List<OrderDTO>>(result);
        }
    }
}
