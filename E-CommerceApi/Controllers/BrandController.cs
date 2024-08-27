using BusinessLogic.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace E_CommerceApi.Controllers
{
    [Route("[Controller]s")]
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

        [Authorize(Roles = "admin,user")]
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
        [HttpPost]        
        public IActionResult Create([FromBody] BrandDTO entity)
        {            
            _brandService.Create(entity);
            return Ok();            
        }

        [Authorize(Roles = "admin")]
        [HttpPut("by-id")]
        public IActionResult Update(int id, [FromBody] BrandDTO entity)
        {
            if (id != entity.Id)
            {
                return BadRequest("ID mismatch.");
            }
            
             _brandService.Update(entity);
             return Ok();            
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _brandService.Delete(id);
            return Ok();
        }

        [Authorize(Roles = "admin,user")]
        [HttpGet("by-id{id}")]
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
