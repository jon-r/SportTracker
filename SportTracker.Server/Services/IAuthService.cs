using SportTracker.Server.Models.Users;

namespace SportTracker.Server.Services
{
    public interface IAuthService
    {
        public Task Login(AuthRequest request);
        public Task Logout();
    }
}
