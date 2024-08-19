using DataAccess.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepo;
        public CategoryService(CategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public void CreateCategory(Category entity)
        {
            if (_categoryRepo.GetById(entity.Id) is null && !entity.Id.Equals(null))
                _categoryRepo.Create(entity);
        }

        public void UpdateCategory(Category entity)
        {
            if (_categoryRepo.GetById(entity.Id) is not null)
                _categoryRepo.Update(entity);
        }

        public void DeleteCategory(Category entity)
        {
            if (_categoryRepo.GetById(entity.Id) is not null)
                _categoryRepo.Delete(entity.Id);
        }

        public Category GetCategoryById(Category entity)
        {
            if (_categoryRepo.GetById(entity.Id) is not null)
                return _categoryRepo.GetById(entity.Id);
            return null;
        }

        public List<Category> GetCategories()
        {
            return _categoryRepo.GetAll();
        }
    }
}
