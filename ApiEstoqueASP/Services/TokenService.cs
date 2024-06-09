using ApiEstoqueASP.Models;
using ApiEstoqueASP.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiEstoqueASP.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        Claim[] claims =
        [
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email!),
            new Claim(ClaimTypes.PrimarySid, user.Id),
            new Claim(ClaimTypes.Role, "User"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];

        // Chave a ser usada para codificar o token:
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Jwt:Key"]));

        var signInCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
        (
            expires: DateTime.Now.AddDays(7),
            claims: claims,
            issuer: this._configuration["Jwt:Issuer"],
            audience: this._configuration["Jwt:Audience"],
            signingCredentials: signInCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
