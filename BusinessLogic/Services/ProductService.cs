using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.Repositories;
using BusinessLogic.DTOs;
using Entities;

namespace BusinessLogic.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepo;
        private readonly IMapper _mapper;

        public ProductService(ProductRepository productRepository, IMapper mapper)
        {
            _productRepo = productRepository;
            _mapper = mapper;
        }

        public void Create(ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                throw new ArgumentNullException(nameof(productDTO), "Invalid Value!");
            }

            var product = _mapper.Map<Product>(productDTO);
            _productRepo.Create(product);
        }

        public void Update(ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                throw new ArgumentNullException(nameof(productDTO), "Invalid Value!");
            }

            var existingProduct = _productRepo.GetById(productDTO.Id);
            if (existingProduct == null)
            {
                throw new InvalidOperationException("Product not found.");
            }

            var product = _mapper.Map<Product>(productDTO);
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

        public ProductDTO GetById(int productId)
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

            return _mapper.Map<ProductDTO>(product);
        }

        public List<ProductDTO> GetAll()
        {
            var products = _productRepo.GetAll();
            if (products == null || !products.Any())
            {
                throw new Exception("No Products Found");
            }

            return _mapper.Map<List<ProductDTO>>(products);
        }

        public List<ProductDTO> GetByCategoryId(int categoryId)
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

            return _mapper.Map<List<ProductDTO>>(products);
        }

        public List<ProductDTO> GetByRange(double minValue, double maxValue)
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

            return _mapper.Map<List<ProductDTO>>(products);
        }
    }
}
