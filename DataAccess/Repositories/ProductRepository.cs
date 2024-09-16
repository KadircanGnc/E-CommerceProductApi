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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {

        public ProductRepository(ECommerceDbContext context) : base(context)
        {

        }
        public override Product GetById(int id)
        {
            var result = _context.Products
                .Where(p => p.Id == id)
                .Include(p => p.Comments)
                .FirstOrDefault();

            return result!;
        }
        public List<Product> GetByCategoryId(int categoryId)
        {
            return _context.Products
                .Where(p => p.CategoryId == categoryId)                
                .ToList();
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

        public List<Product> GetByIds(List<int> productIds)
        {
            return _context.Products
                .Where(p => productIds.Contains(p.Id))                
                .ToList();
        }

        public List<Product> OrderByPriceDescending()
        {
            return _context.Products
                .OrderByDescending(p => p.Price)                
                .ToList();
        }

        public List<Product> OrderByPriceAscending()
        {
            return _context.Products
                .OrderBy(p => p.Price)                
                .ToList();
        }
        public List<Product> OrderByDate()
        {
            return _context.Products
                .OrderByDescending(p => p.CreatedDate)                
                .ToList();
        }

        public List<Product> GetByRange(decimal minValue, decimal maxValue)
        {
            var result = _context.Products
                .Where(p => p.Price >= minValue && p.Price <= maxValue)                
                .ToList();

            return result;
        }

        public List<Product> SearchByName(string name)
        {
            var lowerName = name.ToLower();

            var result = _context.Products
                .Where(p => p.Name!.ToLower().Contains(lowerName))                
                .ToList();

            return result;
        }
    }
}

