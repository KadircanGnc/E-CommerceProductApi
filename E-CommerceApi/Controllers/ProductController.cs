using BusinessLogic.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Common.DTOs.Product;

namespace E_CommerceApi.Controllers
{
    [Route("[Controller]s")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;        
        public ProductController(IProductService service)
        {
            _service = service;            
        }

        //[Authorize]
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

        //[Authorize]
        [HttpGet("paged")]
        public IActionResult GetAllPaged(int pageNumber, int pageSize, string sortBy = "Default")
        {
            var result = _service.GetAllPaged(pageNumber, pageSize, sortBy);
            if (result == null)
            {
                return BadRequest("Invalid value!");
            }
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateProductDTO entity)
        {
            _service.Create(entity);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut("update-by-id")]
        public IActionResult Update([FromBody] UpdateProductDTO entity)
        {
            if (entity.Id <= 0)
            {
                return BadRequest("ID mismatch.");
            }

            _service.Update(entity);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }

        //[Authorize]
        [HttpGet("by-id")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            if (result == null)
            {
                return BadRequest("Invalid Value");
            }
            return Ok(result);
        }

        [HttpGet("search-by-name")]
        public IActionResult SearchByName(string name)
        {
            var result = _service.SearchByName(name);
            if (result == null)
            {
                return BadRequest("Invalid Value");
            }
            return Ok(result);
        }

        // Search products by name (paged)
        [HttpGet("search-by-name-paged")]
        public IActionResult SearchByNamePaged(string name, int pageNumber, int pageSize)
        {
            var result = _service.SearchByNamePaged(name, pageNumber, pageSize);
            if (result == null)
            {
                return BadRequest("No products found for the search term.");
            }
            return Ok(result);
        }

        //[Authorize]
        [HttpGet("by-category-id")]
        public IActionResult GetByCategoryId(int categoryId)
        {
            var result = _service.GetByCategoryId(categoryId);
            if (result == null)
            {
                return BadRequest("Invalid Value");
            }
            return Ok(result);
        }

        // Get products by category (paged)
        [HttpGet("by-category-id-paged")]
        public IActionResult GetByCategoryIdPaged(int categoryId, int pageNumber, int pageSize)
        {
            var result = _service.GetByCategoryIdPaged(categoryId, pageNumber, pageSize);
            if (result == null)
            {
                return BadRequest("No products found for this category.");
            }
            return Ok(result);
        }

        //[Authorize]
        [HttpGet("by-price-range")]
        public IActionResult GetByRange(decimal minValue, decimal maxValue)
        {
            var result = _service.GetByRange(minValue, maxValue);
            if (result == null)
            {
                return BadRequest("Invalid Value");
            }
            return Ok(result);
        }

        //[Authorize]
        [HttpGet("order-by-price-ascending")]
        public IActionResult OrderByPriceAscending()
        {
            var result = _service.OrderByPriceAscending();
            if (result == null)
            {
                return BadRequest("Invalid Value");
            }
            return Ok(result);
        }

        //[Authorize]
        [HttpGet("order-by-price-descending")]
        public IActionResult OrderByPriceDescending()
        {
            var result = _service.OrderByPriceDescending();
            if (result == null)
            {
                return BadRequest("Invalid Value");
            }
            return Ok(result);
        }

        //[Authorize]
        [HttpGet("order-by-date")]
        public IActionResult OrderByDate()
        {
            var result = _service.OrderByDate();
            if (result == null)
            {
                return BadRequest("Invalid Value");
            }
            return Ok(result);
        }
    }
}
