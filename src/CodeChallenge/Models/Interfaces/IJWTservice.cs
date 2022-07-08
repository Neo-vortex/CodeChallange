using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CodeChallenge.Models.Interfaces;

public interface IJWTservice
{
   Task<JwtSecurityToken> GetToken(List<Claim> authClaims);
}