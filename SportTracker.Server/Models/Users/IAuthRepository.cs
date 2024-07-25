namespace SportTracker.Server.Models.Users
{
    public interface IAuthRepository
    {
        AuthResponse Authenticate(AuthRequest authRequest);
    }
}
