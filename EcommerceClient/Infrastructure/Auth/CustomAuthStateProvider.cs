﻿using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EcommerceClient.Infrastructure.Auth
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {        
        private readonly ISessionStorageService _sessionStorage;        

        public CustomAuthStateProvider(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;            
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Retrieve token from session storage
            var token = await _sessionStorage.GetItemAsync<string>("authToken");

            // If no token, set as anonymous
            if (string.IsNullOrEmpty(token))
            {
                var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
                return new AuthenticationState(anonymousUser);
            }

            // Parse JWT and create claims
            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.ReadJwtToken(token);
            var identity = new ClaimsPrincipal(new ClaimsIdentity(securityToken.Claims, "jwtAuthType"));
                       
            var user = new ClaimsPrincipal(identity);            
            NotifyUserAuthentication(user);
            return new AuthenticationState(user);
        }

        public void NotifyUserAuthentication(ClaimsPrincipal user)
        {
            // Notify Blazor that the authentication state has changed
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void NotifyUserLogout()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
        }
    }
}
