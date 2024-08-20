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
            if (entity == null)
            {
                throw new ArgumentNullException("Invalid Value!");
            }
            _categoryRepo.Create(entity);
        }

        public void UpdateCategory(Category entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Invalid Value!");
            }
            _categoryRepo.Update(entity);
        }

        public void DeleteCategory(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("Invalid Value!");
            }
            _categoryRepo.Delete(id);
        }

        public Category GetCategoryById(int id)
        {
            var result = _categoryRepo.GetById(id);
            if (result == null)
            {
                throw new Exception("Value is null");
            }
            return result;            
        }

        public List<Category> GetCategories()
        {
            var result = _categoryRepo.GetAll();
            if (result == null)
            {
                throw new ArgumentNullException("Value is Null!");
            }
            return result;
        }
    }
}
