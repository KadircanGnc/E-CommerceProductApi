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

        public override void Update(User entity)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == entity.Id);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            user.Name = !string.IsNullOrEmpty(entity.Name) ? entity.Name : user.Name;
            user.Email = !string.IsNullOrEmpty(entity.Email) ? entity.Email : user.Email;
            user.Password = !string.IsNullOrEmpty(entity.Password) ? entity.Password : user.Password;
            user.Address = !string.IsNullOrEmpty(entity.Address) ? entity.Address : user.Address;

            _context.SaveChanges();
        }

    }
}
