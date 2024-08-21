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
        private readonly ProductRepository _productRepository;
        private readonly OrderProductRepository _opRepository;
        private readonly IMapper _mapper;

        public OrderService(OrderRepository orderRepository, IMapper mapper, ProductRepository productRepository, OrderProductRepository op)
        {
            _orderRepository = orderRepository;         
            _productRepository = productRepository;
            _opRepository = op;
            _mapper = mapper;
        }

        public void CreateOrderWithProducts(List<int> productIds)
        {
            if (productIds == null || productIds.Count == 0)
            {
                throw new ArgumentException("Product IDs cannot be null or empty!");
            }

            // Create a new order with the current date
            var order = new Order
            {
                CreateDate = DateTime.UtcNow
            };

            _orderRepository.Create(order);

            // Fetch products based on IDs
            var products = _productRepository.GetProductsByIds(productIds);
            if (products == null || products.Count == 0)
            {
                throw new InvalidOperationException("No products found for the given IDs.");
            }

            // Add products to the newly created order through OrderProduct
            foreach (var product in products)
            {
                var orderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = product.Id
                };

                _opRepository.Create(orderProduct);
            }

            // Calculate the total amount of the order
            order.TotalAmount = products.Sum(p => p.Price);

            // Update the order with the total amount
            _orderRepository.Update(order);
        }
        

        public void UpdateOrder(OrderDTO orderDTO)
        {
            if (orderDTO == null)
            {
                throw new ArgumentNullException("Value is Null!");
            }
            
            var order = _mapper.Map<Order>(orderDTO);
            _orderRepository.Update(order);
        }

        public void DeleteOrder(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("Value is Invalid!");
            }
            _orderRepository.Delete(id);
        }

        public GetOrderDTO GetOrderById(int id)
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
            
            return _mapper.Map<GetOrderDTO>(result);
        }

        public List<GetOrderDTO> GetOrders()
        {
            var result = _orderRepository.GetAll();
            if (result == null)
            {
                throw new ArgumentNullException("Value is Null!");
            }
            
            return _mapper.Map<List<GetOrderDTO>>(result);
        }
    }
}
