using Authentication.Services;
using BusinessLogic.Services;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("[Controller]s")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly ECommerceDbContext _context;

    public AuthenticationController(TokenService tokenService, ECommerceDbContext context)
    {
        _tokenService = tokenService;
        _context = context;
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        //Check the user credentials
        var user = _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
        if (user == null)
        {
            return Unauthorized();
        }

        var token = _tokenService.GenerateToken(user.Id.ToString(), email, user.Role!);
        return Ok(new { token });
        
    }
    
}

