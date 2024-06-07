using ApiEstoqueASP.Models;
using ApiEstoqueASP.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiEstoqueASP.Services;

public class TokenService : ITokenService
{
    public string GenerateToken(User user)
    {
        Claim[] claims =
        [
            new Claim(ClaimTypes.PrimarySid, user.Id),
            new Claim(ClaimTypes.Role, "User")
        ];

        // Chave a ser usada para codificar o token:
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("23857DJSKDNFUJEN85418785DSDHisnjfji39845820945JDJIKGHJJSHNF"));

        var signInCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
        (
            expires: DateTime.Now.AddDays(7),
            claims: claims,
            signingCredentials: signInCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
