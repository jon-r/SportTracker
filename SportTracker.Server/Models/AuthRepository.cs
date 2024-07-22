using System.Security.Authentication;

namespace SportTracker.Server.Models
{
    public class AuthRepository(AppDbContext appDbContext) : IAuthRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public AuthResponse Authenticate(AuthRequest authRequest)
        {
            var user = _appDbContext.Users.SingleOrDefault(u => u.Username == authRequest.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(authRequest.Password, user?.PasswordHash))
            {
                throw new AuthenticationException($"Incorrect username/password");
            }

            return new() { Username = user.Username, Name = user.Name };
        }
    }
}
