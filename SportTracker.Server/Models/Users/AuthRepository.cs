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

            return user!;
        }

        public void UpdatePassword(AuthUpdateRequest authUpdateRequest)
        {
            var user = Authenticate(authUpdateRequest);

            user.SetHashedPassword(authUpdateRequest.NewPassword);

            _appDbContext.Users.Entry(user).CurrentValues.SetValues(user);
            _appDbContext.SaveChanges();
        }

        public User Register(User user)
        {
            user.SetHashedPassword(user.Password);

            var result = _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();

            return result.Entity;
        }

        public bool GetUserExists()
        {
            return _appDbContext.Users.Any();
        }
    }
}
