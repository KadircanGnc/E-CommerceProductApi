using DataAccess.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Common.DTOs.Category;
using BusinessLogic.Interfaces;

namespace BusinessLogic.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public void Create(CreateCategoryDTO createCategoryDTO)
        {
            if (createCategoryDTO == null)
            {
                throw new ArgumentNullException(nameof(createCategoryDTO), "Invalid Value!");
            }
            var category = _mapper.Map<Category>(createCategoryDTO);
            _categoryRepo.Create(category);
        }

        public void Update(UpdateCategoryDTO updateCategoryDTO)
        {
            if (updateCategoryDTO == null)
            {
                throw new ArgumentNullException(nameof(updateCategoryDTO), "Invalid Value!");
            }
            var category = _mapper.Map<Category>(updateCategoryDTO);
            _categoryRepo.Update(category);
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID!", nameof(id));
            }
            _categoryRepo.Delete(id);
        }

        public GetCategoryDTO GetById(int id)
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
            return _mapper.Map<GetCategoryDTO>(category);
        }

        public List<GetCategoryDTO> GetAll()
        {
            var categories = _categoryRepo.GetAll();
            if (categories == null || !categories.Any())
            {
                throw new KeyNotFoundException("No categories found.");
            }
            return _mapper.Map<List<GetCategoryDTO>>(categories);
        }
    }
}
