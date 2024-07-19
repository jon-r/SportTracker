using SportTracker.Server.Models;
using SportTracker.Shared.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;

namespace SportTracker.Server.Services
{
    public class AuthHanderService(IHttpContextAccessor httpContextAccessor, IAuthRepository authRepository) : IAuthHandlerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        private readonly IAuthRepository _authRepository = authRepository;

        public async Task Login(AuthRequest authRequest)
        {
            AuthResponse response = _authRepository.Authenticate(authRequest);
            var claims = new List<Claim>
            {
              new Claim(type: ClaimTypes.Name, response.Username),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = true
                });
        }

        public async Task Logout()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync();
        }

    }
}