using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging.Abstractions;
using SportTracker.Server.Models.Users;

namespace SportTracker.Server.Components.Pages
{
    public partial class UpdatePassword
    {
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
                // todo get username from context
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
