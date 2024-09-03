using BusinessLogic.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using BusinessLogic.DTOs.Brand;

namespace E_CommerceApi.Controllers
{
    [Route("[Controller]s")]
    [ApiController]
    public class BrandController : ControllerBase
    {        
        private readonly IBrandService _brandService;        
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;            
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _brandService.GetAll();
            if (result == null)
            {
                return BadRequest("Invalid Value!");
            }
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("create")]        
        public IActionResult Create([FromBody] CreateBrandDTO entity)
        {            
            _brandService.Create(entity);
            return Ok();            
        }

        [Authorize(Roles = "admin")]
        [HttpPut("update-by-id")]
        public IActionResult Update([FromBody] UpdateBrandDTO entity)
        {
            if (entity.Name == null)
            {
                return BadRequest("ID mismatch.");
            }
            
             _brandService.Update(entity);
             return Ok();            
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            _brandService.Delete(id);
            return Ok();
        }

        [Authorize]
        [HttpGet("by-id")]
        public IActionResult GetById(int id)
        {
            var result = _brandService.GetById(id);
            if (result == null)
            {
                return BadRequest("Invalid Value");
            }
            return Ok(result);
        }
    }
}
