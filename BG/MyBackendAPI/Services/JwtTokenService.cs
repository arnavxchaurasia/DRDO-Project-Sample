using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MyBackendAPI.Models;

namespace MyBackendAPI.Services
{
    public class JwtTokenService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly int _expiresMinutes;

        public JwtTokenService(IConfiguration config)
        {
            _key = config["Jwt:Key"]
                ?? throw new InvalidOperationException("Jwt:Key is not configured. Set the Jwt__Key environment variable.");
            _issuer = config["Jwt:Issuer"] ?? "DrdoProjectSample";
            _expiresMinutes = int.TryParse(config["Jwt:ExpiresMinutes"], out var m) ? m : 60;

            if (Encoding.UTF8.GetByteCount(_key) < 32)
            {
                throw new InvalidOperationException("Jwt:Key must be at least 32 bytes (256 bits) for HMAC-SHA256.");
            }
        }

        public string CreateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_expiresMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
