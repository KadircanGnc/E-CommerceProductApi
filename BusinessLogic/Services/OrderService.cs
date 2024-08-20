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
            if (entity == null)
            {
                throw new ArgumentNullException("Invalid Value!");
            }
            _orderRepository.Create(entity);
            orderProduct.OrderId = entity.Id;            
        }

        public void UpdateOrder(Order entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Value is Null!");
            }
            _orderRepository.Update(entity);
        }

        public void DeleteOrder(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("Value is Invalid!");
            }
            _orderRepository.Delete(id);
        }
        public Order GetOrderById(int id)
        {
            var result = _orderRepository.GetById(id);
            if (result == null)
            {
                throw new ArgumentNullException("Value is Null!");
            }
            return result;          
        }

        public List<Order> GetOrders()
        {
            var result = _orderRepository.GetAll();
            if (result == null)
            {
                throw new ArgumentNullException("Value is Null!");
            }
            return result;
        }
    }
}
