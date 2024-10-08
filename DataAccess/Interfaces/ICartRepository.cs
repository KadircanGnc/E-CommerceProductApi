﻿using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart GetByUserId(int userId);
        int GetItemCount(int cartId);
        List<CartItem> GetCartItems(int cartId);

	}
}
