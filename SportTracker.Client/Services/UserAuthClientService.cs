using SportTracker.Client.Shared;
using SportTracker.Shared.Models;
using SportTracker.Shared.Services;

namespace SportTracker.Client.Services
{
    public class UserAuthClientService(IHttpService httpService, ICookiesService cookiesService)
        : IUserAuthService
    {
        private readonly IHttpService _httpService = httpService;
        private readonly ICookiesService _cookiesService = cookiesService;

        public async Task LoginAsync(AuthRequest authReq)
        {
            var user = await _httpService.Post<AuthResponse>("/api/auth/login", authReq);
            await _cookiesService.Set(Cookies.Jwt, user.Token, 4);
        }

        public async Task LogoutAsync()
        {
            await _cookiesService.Delete(Cookies.Jwt); // todo make cookies as enum?
        }
    }
}
