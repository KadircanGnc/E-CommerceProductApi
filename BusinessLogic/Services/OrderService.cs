using DataAccess.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // EKSİK
        public void CreateOrder(Order entity, OrderProduct orderProduct)
        {
            if (_orderRepository.GetById(entity.Id) is null && entity.Products.Count(x => x.StockCount > 0 ) > 0)
            {
                _orderRepository.Create(entity);
                orderProduct.OrderId = entity.Id;                
            }
        }

        public void UpdateOrder(Order entity)
        {
            if (_orderRepository.GetById(entity.Id) is not null)            
                _orderRepository.Update(entity);            
        }

        public void DeleteOrder(Order entity)
        {
            if (_orderRepository.GetById(entity.Id) is not null)
                _orderRepository.Delete(entity.Id);
        }
        public Order GetOrderById(Order entity)
        {
            if (_orderRepository.GetById(entity.Id) is not null)
                return _orderRepository.GetById(entity.Id);

            return null;
        }

        public List<Order> GetOrders()
        {
            return _orderRepository.GetAll();
        }
    }
}
