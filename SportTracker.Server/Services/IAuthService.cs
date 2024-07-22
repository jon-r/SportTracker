using SportTracker.Server.Models;

namespace SportTracker.Server.Services
{
    public interface IAuthService
    {
        public Task Login(AuthRequest request);
    }
}
