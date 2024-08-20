using DataAccess.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class BrandRepository : GenericRepository<Brand>
    {
        public BrandRepository(ECommerceDbContext context) : base(context)
        {

        }
        public List<Product> GetProductsByBrandId(int brandId)
        {
            var brandProducts = _context.Products
                .Where(p => p.BrandId == brandId)
                .ToList();
            return brandProducts;
        }
    }
}
