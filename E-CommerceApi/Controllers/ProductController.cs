using BusinessLogic.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTOs;

namespace E_CommerceApi.Controllers
{
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;
        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<ProductDTO> GetAllProducts()
        {
            var result = _service.GetProducts();
            if (result == null)
            {
                throw new ArgumentNullException("Invalid value!");
            }
            return result;
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDTO entity)
        {
            try
            {
                _service.CreateProduct(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDTO entity)
        {
            if (id != entity.Id)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                _service.UpdateProduct(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                _service.DeleteProduct(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ProductDTO GetProductById(int id)
        {
            var result = _service.GetById(id);
            if (result == null)
            {
                throw new ArgumentNullException("Invalid Value");
            }
            return result;
        }

        [HttpGet("/ProductCategory/{categoryId}")]
        public List<ProductDTO> GetProductsByCategoryId(int categoryId)
        {
            var result = _service.GetProductsByCategoryId(categoryId);
            if (result == null)
            {
                throw new ArgumentNullException("Invalid Value");
            }
            return result;
        }

        [HttpGet("/ByRange/")]
        public List<ProductDTO> GetProductsByRange(double minValue, double maxValue)
        {
            var result = _service.GetProductsByRange(minValue, maxValue);
            if (result == null)
            {
                throw new ArgumentNullException("Invalid Value");
            }
            return result;
        }
    }
}
