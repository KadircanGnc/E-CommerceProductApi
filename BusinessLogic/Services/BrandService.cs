using DataAccess.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class BrandService
    {
        private readonly BrandRepository _brandRepo;
        public BrandService(BrandRepository brandRepo)
        {
            _brandRepo = brandRepo;
        }
        public void CreateBrand(Brand entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Invalid Value!");
            }
            _brandRepo.Create(entity);
        }
        public void UpdateBrand(Brand entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Invalid Value!");
            }
            _brandRepo.Update(entity);
        }
        public void DeleteBrand(Brand entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Invalid Value!");
            }            
                _brandRepo.Delete(entity.Id);
        }
        public Brand GetBrand(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("Invalid ID!");
            }
            return(_brandRepo.GetById(id));            
        }
        public List<Brand> GetBrands()
        {
            var allBrands = _brandRepo.GetAll();
            if (allBrands == null)
            {
                throw new ArgumentNullException("No Brand Found!");
            }
            return allBrands;
        }
        public List<Product> GetProductsByBrandId(int brandId)
        {
            var products = _brandRepo.GetProductsByBrandId(brandId);
            if (products == null)
            {
                throw new ArgumentNullException("No Product Found!");
            }
            return products;
        }
    }
}
