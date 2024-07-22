namespace SportTracker.Server.Models
{
    public class AuthRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class AuthResponse
    {
        public required string Name { get; set; }
        public required string Username { get; set; }
    }
}
