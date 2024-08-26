using BusinessLogic.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTOs;
using FluentValidation;

namespace E_CommerceApi.Controllers
{
    [Route("users")]
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

        [HttpGet]
        public List<UserDTO> GetAllUsers()
        {
            var allUsers = _userService.GetUsers();
            if (allUsers == null)
                throw new ArgumentNullException("No User Found");

            return allUsers;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDTO entity)
        {
            try
            {
                _userService.CreateUser(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/byUser{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDTO entity)
        {
            if (id != entity.Id)
            {
                return BadRequest("ID mismatch!");
            }
            try
            {
                _userService.UpdateUser(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/byUser{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/byUser{id}")]
        public UserDTO GetUserById(int id)
        {
            if (id == 0)
                throw new ArgumentNullException("id");

            return(_userService.GetById(id));            
        }        

        [HttpGet("/ordersByUser{id}")]
        public List<ProductDTO> GetUserOrdersById(int id)
        {
            var userOrders = _userService.GetOrdersByUserId(id);
            if (userOrders == null)
                throw new ArgumentNullException("No Orders Found");

            return userOrders;            
        } 
    }
}
