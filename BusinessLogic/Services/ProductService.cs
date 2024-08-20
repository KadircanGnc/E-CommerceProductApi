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
            // if (entity != null && entity.Id != 0 && entity.BrandId != 0 && entity.CategoryId != 0)
            _productRepo.Create(entity);
        }
        public void UpdateProduct(Product entity)
        {
            if (_productRepo.GetById(entity.Id) is not null)
                _productRepo.Update(entity);
        }
        public void DeleteProduct(int id)
        {
            _productRepo.Delete(id);
        }
        public Product GetById(int productId)
        {
            if (productId != 0)
                return _productRepo.GetById(productId);

            return null;
        }
        public List<Product> GetProducts()
        {
            return _productRepo.GetAll();
        }

        public List<Product> GetProductsByCategoryId(int categoryId)
        {
            if (categoryId != 0)
                return _productRepo.GetByCategoryId(categoryId).ToList();
            return null;
        }
    }
}
