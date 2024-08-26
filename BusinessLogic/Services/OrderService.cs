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
                // Check if there is enough stock
                if (product.StockCount <= 0)
                {
                    throw new InvalidOperationException($"Product '{product.Name}' is out of stock.");
                }

                // Decrease the stock count
                product.StockCount -= 1;

                // Update the product in the repository
                _productRepository.Update(product);

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
        

        public void AddProductsToOrder(int orderId, List<int> productIds)
        {
            if (orderId <= 0)
            {
                throw new ArgumentException("Invalid order ID.");
            }

            if (productIds == null || productIds.Count == 0)
            {
                throw new ArgumentException("Product IDs cannot be null or empty.");
            }

            // Fetch the existing order
            var order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                throw new InvalidOperationException("Order not found.");
            }

            // Fetch the products to add
            var products = _productRepository.GetProductsByIds(productIds);
            if (products == null || products.Count == 0)
            {
                throw new InvalidOperationException("No products found for the given IDs.");
            }

            // Add products to the existing order
            foreach (var product in products)
            {
                // Check if the product is already associated with the order
                if (!order.OrderProducts.Any(op => op.ProductId == product.Id))
                {
                    var orderProduct = new OrderProduct
                    {
                        OrderId = order.Id,
                        ProductId = product.Id
                    };

                    _opRepository.Create(orderProduct);
                }
            }

            // Optionally, recalculate the total amount of the order
            order.TotalAmount = order.OrderProducts.Sum(op => op.Product!.Price);
            _orderRepository.Update(order);
        }

        public void RemoveProductsFromOrder(int orderId, List<int> productIds)
        {
            if (orderId <= 0)
            {
                throw new ArgumentException("Invalid order ID.");
            }

            if (productIds == null || productIds.Count == 0)
            {
                throw new ArgumentException("Product IDs cannot be null or empty.");
            }

            // Fetch the existing order
            var order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                throw new InvalidOperationException("Order not found.");
            }

            // Fetch the order products to be removed
            var orderProductsToRemove = order.OrderProducts
                .Where(op => productIds.Contains(op.ProductId))
                .ToList();

            if (orderProductsToRemove.Count == 0)
            {
                throw new InvalidOperationException("No products found to remove.");
            }

            // Remove the products from the order
            foreach (var orderProduct in orderProductsToRemove)
            {
                _opRepository.Delete(orderProduct.Id); // Assume this method exists
            }

            // Optionally, recalculate the total amount of the order
            order.TotalAmount = order.OrderProducts.Sum(op => op.Product!.Price);
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
