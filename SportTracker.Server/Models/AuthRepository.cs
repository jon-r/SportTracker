using SportTracker.Server.Services;
using SportTracker.Shared.Models;

namespace SportTracker.Server.Models
{
    public class AuthRepository(AppDbContext appDbContext, IJwtService jwtService) : IAuthRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        private readonly IJwtService _jwtService = jwtService;

        public AuthResponse Authenticate(AuthRequest authRequest)
        {
            var user = _appDbContext.Users.SingleOrDefault(u => u.Username == authRequest.Username);

            Console.WriteLine(user?.PasswordHash);

            if (user == null || !BCrypt.Net.BCrypt.Verify(authRequest.Password, user.PasswordHash))
            {
                throw new ApplicationException("Incorrect username/password");
            }

            AuthResponse response =
                new() { Username = user.Username, Token = _jwtService.GenerateToken(user) };

            return response;
        }
    }
}
