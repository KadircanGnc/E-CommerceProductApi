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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ECommerceDbContext context) : base(context) { }

        public override Category GetById(int id)
        {
            var result = _context.Categories
                           .Include(c => c.Products)
                           .FirstOrDefault(c => c.Id == id);

            return result!;
        }

        public override List<Category> GetAll()
        {
            return _context.Categories
                           .Include(c => c.Products) 
                           .ToList();
        }
    }
}
