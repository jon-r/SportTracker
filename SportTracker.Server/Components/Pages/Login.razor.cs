using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using SportTracker.Server.Models.Users;
using System.Security.Claims;

namespace SportTracker.Server.Components.Pages
{
    public partial class Login
    {
        [Inject]
        private IHttpContextAccessor HttpContextAccessor { get; set; } = null!;

        [Inject]
        private IAuthRepository AuthRepository { get; set; } = null!;

        private string? errorMessage;

        private bool UserExists => AuthRepository.GetUserExists();

        [SupplyParameterFromForm]
        public AuthRequest LoginInput { get; set; } = new();

        private async Task OnSubmitLogin()
        {
            try
            {
                User response = AuthRepository.Authenticate(LoginInput);
                var claims = new List<Claim> { new(type: ClaimTypes.Name, response.Username) };
                var identity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );

                await HttpContextAccessor.HttpContext!.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties { IsPersistent = true }
                );
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
