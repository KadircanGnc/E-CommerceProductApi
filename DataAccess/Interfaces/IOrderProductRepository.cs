using Entities;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IOrderProductRepository : IRepository<OrderProduct>
    {
        void Add(OrderProduct orderProduct);
        void RemoveByOrderId(int orderId);
        List<OrderProduct> GetByOrderId(int orderId);
        List<OrderProduct> GetByProductId(int productId);
    }
}
