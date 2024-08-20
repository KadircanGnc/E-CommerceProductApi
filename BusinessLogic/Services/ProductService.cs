using DataAccess.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepo;
        public ProductService(ProductRepository productRepository)
        {
            _productRepo = productRepository;
        }
        public void CreateProduct(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Invalid Value!");
            }
            _productRepo.Create(entity);
        }
        public void UpdateProduct(Product entity)
        {
            var product = _productRepo.GetById(entity.Id);
            if (product == null)
            {
                throw new InvalidOperationException("Product not found.");
            }
            _productRepo.Update(entity);
        }

        public void DeleteProduct(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("Invalid Value");
            }
            _productRepo.Delete(id);
        }
        public Product GetById(int productId)
        {
            var result = _productRepo.GetById(productId);
            if (result == null)
            {
                throw new Exception("Invalid Value");
            }
            return result;
        }
        public List<Product> GetProducts()
        {
            var result = _productRepo.GetAll();
            if (result == null)
            {
                throw new Exception("Invalid Value");
            }
            return result;
        }
        public List<Product> GetProductsByCategoryId(int categoryId)
        {
            var result = _productRepo.GetByCategoryId(categoryId).ToList();
            if (result == null)
            {
                throw new Exception("Invalid Value");
            }
            return result;
        }
    }
}
