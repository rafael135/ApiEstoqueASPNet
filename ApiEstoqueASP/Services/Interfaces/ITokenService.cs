using ApiEstoqueASP.Models;

namespace ApiEstoqueASP.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
