using System.Text.Json.Serialization;

namespace SportTracker.Server.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public string? PasswordHash { get; set; }
    }
}
