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
            if (_brandRepo.GetById(entity.Id) is null && !entity.Id.Equals(null))
                _brandRepo.Create(entity);
        }
        public void UpdateBrand(Brand entity)
        {
            if (_brandRepo.GetById(entity.Id) is not null)
                _brandRepo.Update(entity);
        }
        public void DeleteBrand(Brand entity)
        {
            if (_brandRepo.GetById(entity.Id) is not null)
                _brandRepo.Delete(entity.Id);
        }
        public Brand GetBrand(int id)
        {
            if (_brandRepo.GetById(id) is not null)
                _brandRepo.GetById(id);

            throw new Exception("ID is not valid");
        }
        public List<Brand> GetBrands()
        {
            return _brandRepo.GetAll();
        }
        public List<Product> GetProductsByBrandId(int brandId)
        {
            if (brandId != 0)
                return _brandRepo.GetProductsByBrandId(brandId);

            throw new Exception("ID is not valid");
        }
    }
}
