using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Librarian.Core.DataTransfertObject.GatewayResponses.Services
{
    public interface IJwtService
    {
        string GenerateSecurityToken(ClaimsIdentity claims);
        JwtSecurityToken DecryptJWTToken(string token);
    }
}
