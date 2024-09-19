using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BusinessLogic.Services;
using DataAccess;
using DataAccess.Repositories;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ECommerceDbContext _context;

        public TokenService(IConfiguration configuration, ECommerceDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }       

        public string GenerateToken(string userId, string userName, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName),                
                new Claim(ClaimTypes.Role, role)
            };

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role)
            };

            var token = GenerateToken(claims);            

            return token.Item1;            

        }

        private (string, DateTime) GenerateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Define the expiration time
            var expiry = DateTime.Now.AddDays(10);
            var expiry2 = new DateTime(expiry.Year, expiry.Month, expiry.Day, expiry.Hour, expiry.Minute, expiry.Second);

            // Retrieve the issuer and audience from the configuration
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            // Create the token with claims, issuer, audience, expiration, and credentials
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiry2,
                signingCredentials: creds
                
            );

            // Return the generated token and its expiration time
            return (new JwtSecurityTokenHandler().WriteToken(token), expiry2);
        }

        public User GetUser(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            return user!;
        }

        public string GetRoleFromToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return string.Empty;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Read and parse the token
                var jwtToken = tokenHandler.ReadJwtToken(token);

                // Extract the role claim
                var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                return roleClaim ?? string.Empty;
            }
            catch (Exception)
            {
                // Handle any token parsing errors
                return string.Empty;
            }
        }
    }
}
