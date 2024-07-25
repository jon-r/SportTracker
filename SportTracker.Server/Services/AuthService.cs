using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SportTracker.Server.Models.Users;

namespace SportTracker.Server.Services
{
    public class AuthService(
        IHttpContextAccessor httpContextAccessor,
        IAuthRepository authRepository
    ) : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        private readonly IAuthRepository _authRepository = authRepository;

        public async Task Login(AuthRequest authRequest)
        {
            User response = _authRepository.Authenticate(authRequest);
            var claims = new List<Claim> { new Claim(type: ClaimTypes.Name, response.Name), };
            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties { IsPersistent = true }
            );
        }

        public async Task Logout()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync();
        }
    }
}
