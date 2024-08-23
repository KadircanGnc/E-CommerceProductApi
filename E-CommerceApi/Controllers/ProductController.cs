using BusinessLogic.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTOs;
using FluentValidation;

namespace E_CommerceApi.Controllers
{
    [Route("api/product")]
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

        [HttpGet("AllProducts")]
        public List<ProductDTO> GetAllProducts()
        {
            var result = _service.GetProducts();
            if (result == null)
            {
                throw new ArgumentNullException("Invalid value!");
            }
            return result;
        }

        [HttpPost("Product")]
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

        [HttpPut("/ByProduct{id}")]
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

        [HttpDelete("/ByProduct{id}")]
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

        [HttpGet("/ByProduct{id}")]
        public ProductDTO GetProductById(int id)
        {
            var result = _service.GetById(id);
            if (result == null)
            {
                throw new ArgumentNullException("Invalid Value");
            }
            return result;
        }

        [HttpGet("/ProductByCategory{categoryId}")]
        public List<ProductDTO> GetProductsByCategoryId(int categoryId)
        {
            var result = _service.GetProductsByCategoryId(categoryId);
            if (result == null)
            {
                throw new ArgumentNullException("Invalid Value");
            }
            return result;
        }

        [HttpGet("/ProductByPriceRange")]
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
