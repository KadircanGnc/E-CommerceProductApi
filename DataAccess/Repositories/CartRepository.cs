﻿using DataAccess.Interfaces;
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

        public int GetItemCount()
        {
            var result = _context.Carts.Include(c => c.CartItems!)
                .FirstOrDefault();            

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
