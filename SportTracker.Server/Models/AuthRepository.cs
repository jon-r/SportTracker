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

            if (user == null || !BCrypt.Net.BCrypt.Verify(authRequest.Password, user.PasswordHash))
            {
                throw new ApplicationException("Incorrect username/password");
            }

            AuthResponse response =
                new() { Username = user.Username, Token = _jwtService.GenerateToken(user) };

            return response;
        }

        public async Task<User> AddUserAsync(User user)
        {
            // todo some better way to add the first/only user
            if (_appDbContext.Users.Any())
            {
                throw new ApplicationException("User already created");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = "[HASHED]";

            var result = await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
