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
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(ECommerceDbContext context) : base(context)
        {

        }

      /*  public override List<Order> GetAll()
        {
            return _context.Orders
            .Include(o => o.Products) 
            .ToList();
        } */
    }
}
