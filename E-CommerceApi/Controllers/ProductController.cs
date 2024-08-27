using BusinessLogic.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTOs;
using FluentValidation;

namespace E_CommerceApi.Controllers
{
    [Route("[Controller]s")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;
        private readonly IValidator<ProductDTO> _validator;
        public ProductController(ProductService service, IValidator<ProductDTO> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _service.GetAll();
            if (result == null)
            {
                return BadRequest("Invalid value!");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductDTO entity)
        {
            _service.Create(entity);
            return Ok();
        }

        [HttpPut("by-id")]
        public IActionResult Update(int id, [FromBody] ProductDTO entity)
        {
            if (id != entity.Id)
            {
                return BadRequest("ID mismatch.");
            }

            _service.Update(entity);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }

        [HttpGet("by-id{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            if (result == null)
            {
                return BadRequest("Invalid Value");
            }
            return Ok(result);
        }

        [HttpGet("by-category-id{categoryId}")]
        public IActionResult GetByCategoryId(int categoryId)
        {
            var result = _service.GetByCategoryId(categoryId);
            if (result == null)
            {
                return BadRequest("Invalid Value");
            }
            return Ok(result);
        }

        [HttpGet("by-price-range")]
        public IActionResult GetByRange(double minValue, double maxValue)
        {
            var result = _service.GetByRange(minValue, maxValue);
            if (result == null)
            {
                return BadRequest("Invalid Value");
            }
            return Ok(result);
        }
    }
}
