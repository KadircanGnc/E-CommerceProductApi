using DataAccess.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLogic.DTOs;

namespace BusinessLogic.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(CategoryRepository categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public void CreateCategory(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                throw new ArgumentNullException(nameof(categoryDTO), "Invalid Value!");
            }
            var category = _mapper.Map<Category>(categoryDTO);
            _categoryRepo.Create(category);
        }

        public void UpdateCategory(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                throw new ArgumentNullException(nameof(categoryDTO), "Invalid Value!");
            }
            var category = _mapper.Map<Category>(categoryDTO);
            _categoryRepo.Update(category);
        }

        public void DeleteCategory(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID!", nameof(id));
            }
            _categoryRepo.Delete(id);
        }

        public CategoryDTO GetCategoryById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID!", nameof(id));
            }
            var category = _categoryRepo.GetById(id);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }
            return _mapper.Map<CategoryDTO>(category);
        }

        public List<CategoryDTO> GetCategories()
        {
            var categories = _categoryRepo.GetAll();
            if (categories == null || !categories.Any())
            {
                throw new KeyNotFoundException("No categories found.");
            }
            return _mapper.Map<List<CategoryDTO>>(categories);
        }
    }
}
