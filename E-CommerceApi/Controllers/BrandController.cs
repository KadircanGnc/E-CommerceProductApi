using BusinessLogic.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace E_CommerceApi.Controllers
{
    [Route("api/brand")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandService _brandService;
        private readonly IValidator<BrandDTO> _brandValidator;
        public BrandController(BrandService brandService, IValidator<BrandDTO> validator)
        {
            _brandService = brandService;
            _brandValidator = validator;
        }

        [Authorize]
        [HttpGet("AllBrands")]
        public List<BrandDTO> GetAllBrands()
        {
            var result = _brandService.GetBrands();
            if (result == null)
            {
                throw new ArgumentNullException("Invalid Value!");
            }
            return result;
        }

        [HttpPost("Brand")]        
        public IActionResult CreateBrand([FromBody] BrandDTO entity)
        {
            try
            {
                _brandService.CreateBrand(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/ByBrand{id}")]
        public IActionResult UpdateBrand(int id, [FromBody] BrandDTO entity)
        {
            if (id != entity.Id)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                _brandService.UpdateBrand(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/ByBrand{id}")]
        public IActionResult DeleteBrand(int id)
        {
            try
            {
                _brandService.DeleteBrand(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/ByBrand{id}")]
        public BrandDTO GetBrandById(int id)
        {
            var result = _brandService.GetById(id);
            if (result == null)
            {
                throw new ArgumentNullException("Invalid Value");
            }
            return result;
        }
    }
}
