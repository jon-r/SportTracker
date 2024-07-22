using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using SportTracker.Server.Services;
using SportTracker.Shared.Models;

namespace SportTracker.Server.Models
{
    public class AuthRepository(AppDbContext appDbContext /*IJwtService jwtService*/) : IAuthRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;
        
        public AuthResponse Authenticate(AuthRequest authRequest)
        {
            var user = _appDbContext.Users.SingleOrDefault(u => u.Username == authRequest.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(authRequest.Password, user?.PasswordHash))
            {
                throw new AuthenticationException($"Incorrect username/password");
            }

            // todo remove token
            return new() { Username = user.Username, Token = "unused" };
        }
    }
}
