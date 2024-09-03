using BusinessLogic.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using BusinessLogic.DTOs.User;

namespace E_CommerceApi.Controllers
{
    [Route("[Controller]s")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;        
        public UserController(IUserService userService)
        {
            _userService = userService;            
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
        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateUserDTO entity)
        {
            _userService.Create(entity);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut("update-by-id")]
        public IActionResult Update([FromBody] UpdateUserDTO entity)
        {
            if (entity.Id <= 0)
            {
                return BadRequest("ID mismatch!");
            }

            _userService.Update(entity);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("by-id")]
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
