using System.Security.Authentication;

namespace SportTracker.Server.Models.Users
{
    public class AuthRepository(AppDbContext appDbContext) : IAuthRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public User Authenticate(AuthRequest authRequest)
        {
            var user = _appDbContext.Users.SingleOrDefault(u => u.Username == authRequest.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(authRequest.Password, user?.PasswordHash))
            {
                throw new AuthenticationException($"Incorrect username/password");
            }

            return user;
        }

        public User UpdatePassword(AuthUpdateRequest authUpdateRequest) { 
            var user = Authenticate(authUpdateRequest);

            if (user != null)
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(authUpdateRequest.NewPassword);
                user.Password = "**********";


                _appDbContext.Users.Entry(user).CurrentValues.SetValues(user);
                _appDbContext.SaveChanges();
            }

            return user;
        }
    }
}
