namespace SportTracker.Server.Models.Users
{
    public class AuthRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class AuthUpdateRequest : AuthRequest
    {
        public string NewPassword { get; set; } = string.Empty;
    }

    public class AuthResponse
    {
        public required string Name { get; set; }
        public required string Username { get; set; }
    }
}
