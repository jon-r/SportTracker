using SportTracker.Server.Models;

namespace SportTracker.Server.Services
{
    public interface IAuthHandlerService
    {
        public Task Login(AuthRequest request);
    }
}
