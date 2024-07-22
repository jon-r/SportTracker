namespace SportTracker.Server.Models
{
    public interface IAuthRepository
    {
        AuthResponse Authenticate(AuthRequest authRequest);
    }
}
