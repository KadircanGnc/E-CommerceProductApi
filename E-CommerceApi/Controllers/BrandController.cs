using BusinessLogic.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTOs;

namespace E_CommerceApi.Controllers
{
    [Route("[controller]s")]
    public class BrandController : ControllerBase
    {
        private readonly BrandService _brandService;
        public BrandController(BrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public List<BrandDTO> GetAllBrands()
        {
            var result = _brandService.GetBrands();
            if (result == null)
            {
                throw new ArgumentNullException("Invalid Value!");
            }
            return result;
        }

        [HttpPost]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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

        [HttpGet("{id}")]
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
