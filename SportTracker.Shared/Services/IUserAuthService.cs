using SportTracker.Shared.Models;

namespace SportTracker.Shared.Services
{
    public interface IUserAuthService
    {
        Task LoginAsync(AuthRequest authReq);
        Task LogoutAsync();
        // todo refresh token
    }
}
