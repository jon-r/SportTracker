using SportTracker.Shared.Models;
using SportTracker.Shared.Services;

namespace SportTracker.Server.Services
{
    public class UserAuthServerService : IUserAuthService
    {
        public async Task LoginAsync(AuthRequest authReq)
        {
            await Task.CompletedTask;
        }

        public async Task LogoutAsync()
        {
            await Task.CompletedTask;
        }
    }
}
