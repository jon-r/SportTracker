using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using SportTracker.Server.Models.Users;
using System.Security.Claims;

namespace SportTracker.Server.Components.Pages
{
    public partial class Register
    {
        // todo some way to limit adding extra users

        [Inject]
        private IHttpContextAccessor HttpContextAccessor { get; set; } = null!;

        [Inject]
        private IAuthRepository AuthRepository { get; set; } = null!;

        private string? errorMessage;

        [SupplyParameterFromForm]
        public User NewUser { get; set; } =
            new() { Username = string.Empty, Password = string.Empty };

        private bool UserExists => AuthRepository.GetUserExists();

        private async Task OnSubmitRegister()
        {
            if (UserExists)
            {
                // right now project only supports a single user, so blocking future registrations
                errorMessage = "Cannot register multiple users";

                return;
            }

            try
            {
                User response = AuthRepository.Register(NewUser);
                var claims = new List<Claim> { new(type: ClaimTypes.Name, response.Username), };
                var identity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );

                await HttpContextAccessor.HttpContext!.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties { IsPersistent = true, RedirectUri = "/" }
                );
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
