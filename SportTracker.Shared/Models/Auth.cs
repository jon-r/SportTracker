namespace SportTracker.Shared.Models
{
    public class AuthRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class AuthResponse
    {
        public string Username { get; set; }
        public string Token { get; set; }
        // todo refresh token?
    }
}
