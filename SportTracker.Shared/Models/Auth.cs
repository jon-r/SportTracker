namespace SportTracker.Shared.Models
{
    public class AuthRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponse
    {
        public string Username { get; set; }
        public string Token { get; set; }
        // todo refresh token?
    }
}
