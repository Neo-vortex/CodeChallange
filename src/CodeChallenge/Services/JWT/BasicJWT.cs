using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CodeChallenge.Models.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace CodeChallenge.Services.JWT;

public class BasicJWT : IJWTservice
{
    private readonly IConfiguration _configuration;

    public BasicJWT(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Task<JwtSecurityToken> GetToken(List<Claim> authClaims)
    {
        return Task.Run(() => new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])), SecurityAlgorithms.HmacSha512)
        ));
    }
}