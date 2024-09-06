using BusinessLogic.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Common.DTOs.Category;

namespace E_CommerceApi.Controllers
{
    [Route("[Controller]s")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;        
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;            
        }

        [Authorize]
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

        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateCategoryDTO entity)
        {
            _categoryService.Create(entity);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut("update-by-id")]
        public IActionResult Update([FromBody] UpdateCategoryDTO entity)
        {
            if (entity.Id <= 0)
            {
                return BadRequest("ID mismatch.");
            }

            _categoryService.Update(entity);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return Ok();
        }

        [Authorize]
        [HttpGet("by-id")]
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
