﻿using Authentication.Services;
using BusinessLogic.Services;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
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
    public IActionResult Login(string email, string password)
    {
        //Check the user credentials
        var user = _tokenService.GetUser(email, password);
        if (user == null)
        {
            return Unauthorized();
        }

        var token = _tokenService.GenerateToken(user.Id.ToString(), email, user.Role!);
        return Ok(new { token });        
    }
    
}

