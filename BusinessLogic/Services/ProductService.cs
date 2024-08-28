using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.Repositories;
using Entities;
using BusinessLogic.DTOs.Product;
using BusinessLogic.DTOs;

namespace BusinessLogic.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepo;
        private readonly IMapper _mapper;
        private readonly PaginationService _paginationService;

        public ProductService(ProductRepository productRepository, IMapper mapper, PaginationService paginationService)
        {
            _productRepo = productRepository;
            _mapper = mapper;
            _paginationService = paginationService;
        }

        public void Create(CreateProductDTO createProductDTO)
        {
            if (createProductDTO == null)
            {
                throw new ArgumentNullException(nameof(createProductDTO), "Invalid Value!");
            }

            var product = _mapper.Map<Product>(createProductDTO);
            _productRepo.Create(product);
        }

        public void Update(UpdateProductDTO updateProductDTO)
        {
            if (updateProductDTO.Id <= 0)
            {
                throw new ArgumentNullException(nameof(updateProductDTO), "Invalid Value!");
            }

            var existingProduct = _productRepo.GetById(updateProductDTO.Id);
            if (existingProduct == null)
            {
                throw new InvalidOperationException("Product not found.");
            }

            var product = _mapper.Map<Product>(updateProductDTO);
            _productRepo.Update(product);
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id), "Invalid Value");
            }
            _productRepo.Delete(id);
        }

        public GetProductDTO GetById(int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentNullException(nameof(productId), "Invalid Value");
            }

            var product = _productRepo.GetById(productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            return _mapper.Map<GetProductDTO>(product);
        }

          public List<GetProductDTO> GetAll()
          {
              var products = _productRepo.GetAll();
              if (products == null || !products.Any())
              {
                  throw new Exception("No Products Found");
              }

              return _mapper.Map<List<GetProductDTO>>(products);
          }        

        // Example usage of pagination
        public PagedResult<GetProductDTO> GetAllPaged(int pageNumber, int pageSize)
        {
            var query = _productRepo.GetAll().AsQueryable();

            return _paginationService.GetPagedResult<GetProductDTO, Product>(
                query,
                pageNumber,
                pageSize,
                product => _mapper.Map<GetProductDTO>(product)
            );
        }
        public List<GetProductDTO> GetByCategoryId(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentNullException(nameof(categoryId), "Invalid Value");
            }

            var products = _productRepo.GetByCategoryId(categoryId).ToList();
            if (products == null || !products.Any())
            {
                throw new Exception("No Products Found");
            }

            return _mapper.Map<List<GetProductDTO>>(products);
        }

        public List<GetProductDTO> GetByRange(double minValue, double maxValue)
        {
            if (minValue < 0 || maxValue < minValue)
            {
                throw new ArgumentException("Invalid range values.");
            }

            var products = _productRepo._context.Products
                .Where(p => p.Price >= minValue && p.Price <= maxValue)
                .ToList();

            if (products == null || !products.Any())
            {
                throw new Exception("No Products Found");
            }

            return _mapper.Map<List<GetProductDTO>>(products);
        }

        public List<GetProductDTO> OrderByPriceDescending()
        {
            var products = _productRepo.OrderByPriceDescending();

            if (products == null || !products.Any())
            {
                throw new Exception("No Products Found");
            }

            return _mapper.Map<List<GetProductDTO>>(products);
        }

        public List<GetProductDTO> OrderByPriceAscending()
        {
            var products = _productRepo.OrderByPriceAscending();

            if (products == null || !products.Any())
            {
                throw new Exception("No Products Found");
            }

            return _mapper.Map<List<GetProductDTO>>(products);
        }

        public List<GetProductDTO> OrderByDate()
        {
            var products = _productRepo.OrderByDate();

            if (products == null || !products.Any())
            {
                throw new Exception("No Products Found");
            }

            return _mapper.Map<List<GetProductDTO>>(products);
        }

    }
}
