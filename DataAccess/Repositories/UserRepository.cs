using DataAccess.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(ECommerceDbContext context) : base(context)
        {

        }
        public List<Product> GetOrdersByUserId(int userId)
        {
            var orderedProducts = _context.Orders
                .Where(o => o.UserId == userId)
                .SelectMany(o => o.OrderProducts)
                .Select(op => op.Product)
                .Distinct().ToList();

            return orderedProducts;
        }

    }
}
