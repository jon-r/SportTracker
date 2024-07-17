using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SportTracker.Shared.Models;

namespace SportTracker.Server.Services
{
    public interface IJwtService
    {
        public string GenerateToken(User user);
        // todo refresh token. need the client to refresh periodically before the token expires
    }

    public class JwtService : IJwtService
    {
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(
                "50810017cb402db5d4e39d724384cc0dae83fec4b838b796f316a9849ced3639"
            ); // fixme get something better
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([new Claim("id", user.Id.ToString())]),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
