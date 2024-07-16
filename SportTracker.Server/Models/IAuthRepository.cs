using SportTracker.Shared.Models;

namespace SportTracker.Server.Models
{
    public interface IAuthRepository
    {
        AuthResponse Authenticate(AuthRequest authRequest);
        Task<User> AddUserAsync(User user);
    }
}
