using DataAccess.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.DTOs;

namespace BusinessLogic.Services
{
    public class BrandService
    {
        private readonly BrandRepository _brandRepo;
        private readonly IMapper _mapper;

        public BrandService(BrandRepository brandRepo, IMapper mapper)
        {
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        public void CreateBrand(BrandDTO brandDTO)
        {
            if (brandDTO == null)
            {
                throw new ArgumentNullException(nameof(brandDTO), "Invalid Value!");
            }
            var brand = _mapper.Map<Brand>(brandDTO);
            _brandRepo.Create(brand);
        }

        public void UpdateBrand(BrandDTO brandDTO)
        {
            if (brandDTO == null)
            {
                throw new ArgumentNullException(nameof(brandDTO), "Invalid Value!");
            }
            var brand = _mapper.Map<Brand>(brandDTO);
            _brandRepo.Update(brand);
        }

        public void DeleteBrand(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID!", nameof(id));
            }
            _brandRepo.Delete(id);
        }

        public BrandDTO GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID!", nameof(id));
            }
            var brand = _brandRepo.GetById(id);
            if (brand == null)
            {
                throw new KeyNotFoundException("Brand not found.");
            }
            return _mapper.Map<BrandDTO>(brand);
        }

        public List<BrandDTO> GetBrands()
        {
            var allBrands = _brandRepo.GetAll();
            if (allBrands == null || !allBrands.Any())
            {
                throw new KeyNotFoundException("No brands found.");
            }
            return _mapper.Map<List<BrandDTO>>(allBrands);
        }

        public List<ProductDTO> GetProductsByBrandId(int brandId)
        {
            if (brandId <= 0)
            {
                throw new ArgumentException("Invalid Brand ID!", nameof(brandId));
            }
            var products = _brandRepo.GetProductsByBrandId(brandId);
            if (products == null || !products.Any())
            {
                throw new KeyNotFoundException("No products found for this brand.");
            }
            return _mapper.Map<List<ProductDTO>>(products);
        }
    }
}
