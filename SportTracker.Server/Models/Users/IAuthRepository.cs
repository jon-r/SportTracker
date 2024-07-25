namespace SportTracker.Server.Models.Users
{
    public interface IAuthRepository
    {
        User Authenticate(AuthRequest authRequest);

        User UpdatePassword(AuthUpdateRequest authUpdateRequest);
    }
}
