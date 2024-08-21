using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.Repositories;
using Entities;
using BusinessLogic.DTOs;

namespace BusinessLogic.Services
{
    public class OrderProductService
    {
        private readonly OrderProductRepository _orderProductRepo;
        private readonly IMapper _mapper;

        public OrderProductService(OrderProductRepository orderProductRepo, IMapper mapper)
        {
            _orderProductRepo = orderProductRepo;
            _mapper = mapper;
        }

        public void AddOrderProduct(OrderProductDTO orderProductDTO)
        {
            if (orderProductDTO == null)
            {
                throw new ArgumentNullException(nameof(orderProductDTO), "OrderProductDTO cannot be null.");
            }

            var orderProduct = _mapper.Map<OrderProduct>(orderProductDTO);
            _orderProductRepo.AddOrderProduct(orderProduct);
        }

        public void UpdateOrderProducts(int orderId, List<OrderProductDTO> orderProductDTOs)
        {
            if (orderId <= 0)
            {
                throw new ArgumentException("Invalid order ID.", nameof(orderId));
            }

            // Remove existing order-product associations
            _orderProductRepo.RemoveOrderProductsByOrderId(orderId);

            // Add new order-product associations
            foreach (var dto in orderProductDTOs)
            {
                if (dto.ProductId <= 0)
                {
                    throw new ArgumentException("Invalid product ID.", nameof(dto.ProductId));
                }

                var orderProduct = _mapper.Map<OrderProduct>(dto);
                _orderProductRepo.AddOrderProduct(orderProduct);
            }
        }

        public List<OrderProductDTO> GetOrderProductsByOrderId(int orderId)
        {
            if (orderId <= 0)
            {
                throw new ArgumentException("Invalid order ID.", nameof(orderId));
            }

            var orderProducts = _orderProductRepo.GetByOrderId(orderId);
            return _mapper.Map<List<OrderProductDTO>>(orderProducts);
        }

        public List<OrderProductDTO> GetOrderProductsByProductId(int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentException("Invalid product ID.", nameof(productId));
            }

            var orderProducts = _orderProductRepo.GetByProductId(productId);
            return _mapper.Map<List<OrderProductDTO>>(orderProducts);
        }
    }
}
