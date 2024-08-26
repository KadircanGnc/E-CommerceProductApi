using System;
using System.Collections.Generic;
using AutoMapper;
using DataAccess.Repositories;
using BusinessLogic.DTOs;
using Entities;

namespace BusinessLogic.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly CartRepository _cartRepository;
        private readonly ProductRepository _productRepository;
        private readonly OrderProductRepository _opRepository;
        private readonly IMapper _mapper;

        public OrderService(OrderRepository orderRepository, IMapper mapper, ProductRepository productRepository, OrderProductRepository op, CartRepository cartRepository)
        {
            _orderRepository = orderRepository;         
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _opRepository = op;
            _mapper = mapper;
        }

        public void PlaceOrder(int cartId)
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
                CreateDate = DateTime.UtcNow,
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


        public void DeleteOrder(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("Value is Invalid!");
            }
            _orderRepository.Delete(id);
        }

        public OrderDTO GetOrderById(int id)
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

        public List<OrderDTO> GetOrders()
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
