using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
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
            var result = _context.Carts
                .Include(c => c.CartItems!)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.Id == id);

            return result!;
        }

        public int GetItemCount(int cartId)
        {
            var result = _context.Carts
                .Include(c => c.CartItems!)
                .Where(c => c.Id == cartId)
                .FirstOrDefault();

            if (result == null || result.CartItems == null)
            {
                return 0; // Return 0 if the cart or CartItems is null
            }

            return result!.CartItems!.Sum(ci => ci.Quantity);
        }

        public List<CartItem> GetCartItems(int cartId)
        {
            var result = _context.Carts
                .Include(c => c.CartItems!)
                .Where(c => c.Id == cartId)
                .SelectMany(c => c.CartItems!)
                .ToList();

            return result;
        }
    }
}
