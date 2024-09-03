using System.Collections.Generic;
using System.Linq;
using DataAccess;
using DataAccess.Interfaces;
using Entities;

namespace DataAccess.Repositories
{
    public class OrderProductRepository : GenericRepository<OrderProduct>, IOrderProductRepository
    {       

        public OrderProductRepository(ECommerceDbContext context) : base(context) { }       

        public void Add(OrderProduct orderProduct)
        {
            _context.OrderProducts.Add(orderProduct);
            _context.SaveChanges();
        }

        public void RemoveByOrderId(int orderId)
        {
            var orderProducts = _context.OrderProducts.Where(op => op.OrderId == orderId).ToList();
            _context.OrderProducts.RemoveRange(orderProducts);
            _context.SaveChanges();
        }

        public List<OrderProduct> GetByOrderId(int orderId)
        {
            return _context.OrderProducts.Where(op => op.OrderId == orderId).ToList();
        }

        public List<OrderProduct> GetByProductId(int productId)
        {
            return _context.OrderProducts.Where(op => op.ProductId == productId).ToList();
        }
    }
}
