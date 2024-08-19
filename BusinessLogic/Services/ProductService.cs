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
            if (_productRepo.GetById(entity.Id) is null && !entity.Id.Equals(null) && !entity.BrandId.Equals(null) && !entity.CategoryId.Equals(null))
                _productRepo.Create(entity);
        }
        public void UpdateProduct(Product entity)
        {
            if (_productRepo.GetById(entity.Id) is not null)
                _productRepo.Update(entity);
        }
        public void DeleteProduct(Product entity)
        {
            if (_productRepo.GetById(entity.Id) is not null)
                _productRepo.Delete(entity.Id);
        }
        public Product GetById(Product entity)
        {
            if (_productRepo.GetById(entity.Id) is not null)
                return _productRepo.GetById(entity.Id);

            return null;
        }
        public List<Product> GetProducts()
        {
            return _productRepo.GetAll();
        }       
    }
}
