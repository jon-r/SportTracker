using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;

namespace SportTracker.Server.Components.Layout
{
    public partial class UserMenu
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        private IHttpContextAccessor HttpContextAccessor { get; set; } = null!;

        private async Task OnSubmitLogout()
        {
            await HttpContextAccessor.HttpContext.SignOutAsync();

            NavigationManager.NavigateTo("/login");
        }
    }
}
