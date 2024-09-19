using Authentication.Services;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

[Route("[Controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly TokenService _tokenService;  
    
    public AuthenticationController(TokenService tokenService)
    {
        _tokenService = tokenService;        
    }

    [HttpPost]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        //Check the user credentials
        var user = _tokenService.GetUser(loginRequest.Email!, loginRequest.Password!);
        if (user == null)
        {
            return Unauthorized();
        }

        var token = _tokenService.GenerateToken(user.Id.ToString(), loginRequest.Email!, user.Role!);        
        return Ok(new { token });        
    }

    [HttpGet("get-user-role")]
    public IActionResult GetUserRole(string token)
    {
        var result = _tokenService.GetRoleFromToken(token);

        return Ok(result);
    }
    public class LoginRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}

