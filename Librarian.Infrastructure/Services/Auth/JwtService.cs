using Librarian.Core.DataTransfertObject.GatewayResponses.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Librarian.Infrastructure.Services.Auth
{
    public class JwtService : IJwtService
    {
        private readonly string secret;
        private readonly int expDelay;

        public JwtService(IConfiguration configuration)
        {
            this.secret = configuration.GetSection("JwtConfig").GetSection("secret").Value;
            if (!int.TryParse(configuration.GetSection("JwtConfig").GetSection("expirationInMinutes").Value, out this.expDelay))
                this.expDelay = 7;
        }

        public static SymmetricSecurityKey GetSymmetricSecurityKey(string secret)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            return securityKey;
        }

        public string GenerateSecurityToken(ClaimsIdentity claims)
        {
            SymmetricSecurityKey securityKey = JwtService.GetSymmetricSecurityKey(this.secret);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(
                subject: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(this.expDelay),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );
            return handler.WriteToken(token);
        }

        public JwtSecurityToken DecryptJWTToken(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.ReadToken(token) as JwtSecurityToken;
        }
    }
}
