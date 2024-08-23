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
    public class BrandRepository : GenericRepository<Brand>
    {
        public BrandRepository(ECommerceDbContext context) : base(context)
        {

        }
        public override Brand GetById(int id)
        {
            return _context.Brands
                .Include(b => b.Products)
                .FirstOrDefault(b => b.Id == id);
        }
        public override List<Brand> GetAll()
        {
            return _context.Brands
                .Include(b => b.Products)
                .ToList();
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
