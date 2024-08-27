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
    public class UserController : ControllerBase
    {
        private UserService _userService;
        private readonly IValidator<UserDTO> _validator;
        public UserController(UserService userService, IValidator<UserDTO> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var allUsers = _userService.GetAll();
            if (allUsers == null)
            {
                return BadRequest("No User Found");
            }               

            return Ok(allUsers);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create([FromBody] UserDTO entity)
        {
            _userService.Create(entity);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut("by-id")]
        public IActionResult Update(int id, [FromBody] UserDTO entity)
        {
            if (id != entity.Id)
            {
                return BadRequest("ID mismatch!");
            }

            _userService.Update(entity);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("by-id{id}")]
        public IActionResult GetById(int id)
        {
            var result = _userService.GetById(id);
            if (result == null)
            {
                return BadRequest("Invalid value");
            }                

            return Ok(result);            
        }

        [Authorize(Roles = "admin")]
        [HttpGet("orders-by-user-id")]
        public IActionResult GetOrdersByUserId(int id)
        {
            var userOrders = _userService.GetOrdersByUserId(id);
            if (userOrders == null)
            {
                return BadRequest("No Orders Found");
            }                

            return Ok(userOrders);            
        } 
    }
}
