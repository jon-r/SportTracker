using Microsoft.AspNetCore.Components;
using SportTracker.Server.Models.Users;

namespace SportTracker.Server.Components.Pages
{
    public partial class UpdatePassword
    {
        [Inject]
        private IHttpContextAccessor HttpContextAccessor { get; set; } = null!;

        [Inject]
        private IAuthRepository AuthRepository { get; set; } = null!;

        private string? errorMessage;
        private bool submitted = false;

        [SupplyParameterFromForm]
        public AuthUpdateRequest UpdateInput { get; set; } = new();

        private void OnSubmitUpdate()
        {
            try
            {
                UpdateInput.Username = HttpContextAccessor.HttpContext!.User.Identity!.Name!;

                AuthRepository.UpdatePassword(UpdateInput);
                submitted = true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
