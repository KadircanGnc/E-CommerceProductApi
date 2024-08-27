using BusinessLogic.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTOs;
using FluentValidation;

namespace E_CommerceApi.Controllers
{
    [Route("[Controller]s")]
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
        public IActionResult GetAll()
        {
            var result = _categoryService.GetAll();
            if (result == null)
            {
                return BadRequest("Invalid Value!");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CategoryDTO entity)
        {
            _categoryService.Create(entity);
            return Ok();
        }

        [HttpPut(("by-id"))]
        public IActionResult Update(int id, [FromBody] CategoryDTO entity)
        {
            if (id != entity.Id)
            {
                return BadRequest("ID mismatch.");
            }

            _categoryService.Update(entity);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return Ok();
        }

        [HttpGet("by-id{id}")]
        public IActionResult GetById(int id)
        {
            var result = _categoryService.GetById(id);
            if (result == null)
            {
                return BadRequest("Invalid Value");
            }
            return Ok(result);
        }
    }
}
