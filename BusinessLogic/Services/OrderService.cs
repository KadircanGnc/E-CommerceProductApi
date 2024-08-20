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
            if (_orderRepository.GetById(entity.Id) is null)
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

        public void DeleteOrder(int id)
        {
            if (_orderRepository.GetById(id) is not null)
                _orderRepository.Delete(id);
        }
        public Order GetOrderById(int id)
        {
            if (_orderRepository.GetById(id) is not null)
                return _orderRepository.GetById(id);

            throw new Exception("ID is not valid");
        }

        public List<Order> GetOrders()
        {
            return _orderRepository.GetAll();
        }
    }
}
