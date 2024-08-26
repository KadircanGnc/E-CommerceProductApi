using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CartRepository : GenericRepository<Cart>
    {        
        public CartRepository(ECommerceDbContext context) : base(context) { }        

        public Cart GetByUserId(int userId)
        {
            var result = _context.Carts
                .Include(c => c.CartItems!)
                .ThenInclude(ci => ci.Product) 
                .FirstOrDefault(c => c.UserId == userId);

            return result!;
        }

        public override Cart GetById(int id)
        {
            return _context.Carts
                .Include(c => c.CartItems)  
                .ThenInclude(ci => ci.Product)  
                .FirstOrDefault(c => c.Id == id);
        }
    }
}
