using System.Text.Json.Serialization;

namespace SportTracker.Server.Models.Users
{
    public class User
    {
        public int? Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }

        [JsonIgnore]
        public string? PasswordHash { get; set; }

        public void SetHashedPassword(string newPassword)
        {
            Password = "*****";
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        }
    }
}
