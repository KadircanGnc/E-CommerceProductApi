using Authentication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/Authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthenticationController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login(string userName, string password)
    {
        // Replace with your own user validation logic
        if (userName == "admin" && password == "admin")
        {
            var token = _tokenService.GenerateToken("1", userName!);
            return Ok(new { token });
        }

        return Unauthorized();
    }
}

