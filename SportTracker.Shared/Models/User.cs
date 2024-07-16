using System.Text.Json.Serialization;

namespace SportTracker.Shared.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public string? PasswordHash { get; set; }
    }
}
