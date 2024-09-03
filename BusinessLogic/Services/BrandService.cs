using DataAccess.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.DTOs.Brand;
using BusinessLogic.DTOs.Product;
using BusinessLogic.Interfaces;

namespace BusinessLogic.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepo;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepo, IMapper mapper)
        {
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        public void Create(CreateBrandDTO createBrandDTO)
        {
            if (createBrandDTO == null)
            {
                throw new ArgumentNullException(nameof(createBrandDTO), "Invalid Value!");
            }
            var brand = _mapper.Map<Brand>(createBrandDTO);
            _brandRepo.Create(brand);
        }

        public void Update(UpdateBrandDTO updateBrandDTO)
        {
            if (updateBrandDTO.Id <= 0)
            {
                throw new ArgumentNullException(nameof(updateBrandDTO), "Invalid Value!");
            }
            var brand = _mapper.Map<Brand>(updateBrandDTO);
            _brandRepo.Update(brand);
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID!", nameof(id));
            }
            _brandRepo.Delete(id);
        }

        public GetBrandDTO GetById(int id)
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
            return _mapper.Map<GetBrandDTO>(brand);
        }

        public List<GetBrandDTO> GetAll()
        {
            var allBrands = _brandRepo.GetAll();
            if (allBrands == null || !allBrands.Any())
            {
                throw new KeyNotFoundException("No brands found.");
            }
            return _mapper.Map<List<GetBrandDTO>>(allBrands);
        }

        public List<GetProductDTO> GetProductsByBrandId(int brandId)
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
            return _mapper.Map<List<GetProductDTO>>(products);
        }
    }
}
