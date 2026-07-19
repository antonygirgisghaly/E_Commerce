using E_Commerce.Application.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastracture.Identity.Services
{
    internal class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public string CreateToken(string userId, string Email, string userName, IReadOnlyList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, Email),
                new Claim(ClaimTypes.Name, userName)
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var secreatKey = _jwtSettings.SecretKey;
            if (string.IsNullOrWhiteSpace(secreatKey))
                throw new InvalidOperationException("JWT Secret Key is missing");
            if(secreatKey.Length < 32)
                throw new InvalidOperationException("JWT Secret Key must be at least 32 characters long");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secreatKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
        public class JwtSettings 
        {
            public string SecretKey { get; set; } = default!;
            public string Issuer { get; set; } = default!;
            public string Audience { get; set; } = default!;
            public int ExpirationInMinutes { get; set; }
        }
}
