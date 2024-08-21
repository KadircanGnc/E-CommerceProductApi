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
    public class ProductRepository : GenericRepository<Product>
    {

        public ProductRepository(ECommerceDbContext context) : base(context)
        {

        }
        public List<Product> GetByCategoryId(int categoryId)
        {
            return _context.Products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public override void Update(Product entity)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == entity.Id);

            if (product == null)
            {
                throw new InvalidOperationException("Product not found.");
            }

            product.Name = !string.IsNullOrEmpty(entity.Name) ? entity.Name : product.Name;
            product.Price = entity.Price != default ? entity.Price : product.Price;
            product.StockCount = entity.StockCount != default ? entity.StockCount : product.StockCount;

            _context.SaveChanges();
        }

        public List<Product> GetProductsByIds(List<int> productIds)
        {
            return _context.Products.Where(p => productIds.Contains(p.Id)).ToList();
        }

    }
}

