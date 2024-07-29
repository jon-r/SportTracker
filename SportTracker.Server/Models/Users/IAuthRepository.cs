namespace SportTracker.Server.Models.Users
{
    public interface IAuthRepository
    {
        User Authenticate(AuthRequest authRequest);

        void UpdatePassword(AuthUpdateRequest authUpdateRequest);

        User Register(User user);

        bool GetUserExists();
    }
}
