using BusinessLogic.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApi.Controllers
{
    [Route("[controller]s")]
    public class UserController : ControllerBase
    {
        private UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User entity)
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

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User entity)
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

        [HttpDelete("{id}")]
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

        [HttpGet("{id}")]
        public User GetUserById(int id)
        {
            if (id == 0)
                throw new ArgumentNullException("id");

            return(_userService.GetById(id));            
        }

        [HttpGet]
        public List<User> GetAllUsers()
        {
            var allUsers = _userService.GetUsers();
            if (allUsers == null)
                throw new ArgumentNullException("No User Found");

            return allUsers;            
        }

        [HttpGet("/orders/{id}")]
        public List<Product> GetUserOrdersById(int id)
        {
            var userOrders = _userService.GetOrdersByUserId(id);
            if (userOrders == null)
                throw new ArgumentNullException("No Orders Found");

            return userOrders;            
        }
    }
}
