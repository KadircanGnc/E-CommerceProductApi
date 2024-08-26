using BusinessLogic.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTOs;
using FluentValidation;

namespace E_CommerceApi.Controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly IValidator<CategoryDTO> _validator;
        public CategoryController(CategoryService categoryService, IValidator<CategoryDTO> validator)
        {
            _categoryService = categoryService;
            _validator = validator;
        }

        [HttpGet]
        public List<CategoryDTO> GetAllCategories()
        {
            var result = _categoryService.GetCategories();
            if (result == null)
            {
                throw new ArgumentNullException("Invalid Value!");
            }
            return result;
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryDTO entity)
        {
            try
            {
                _categoryService.CreateCategory(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/byCategory{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDTO entity)
        {
            if (id != entity.Id)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                _categoryService.UpdateCategory(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/byCategory{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/byCategory{id}")]
        public CategoryDTO GetCategoryById(int id)
        {
            var result = _categoryService.GetCategoryById(id);
            if (result == null)
            {
                throw new ArgumentNullException("Invalid Value");
            }
            return result;
        }
    }
}
