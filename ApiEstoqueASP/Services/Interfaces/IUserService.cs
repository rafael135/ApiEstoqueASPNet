using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Models;

namespace ApiEstoqueASP.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> Register(CreateUserDto dto);
        Task<User?> Login(LoginUserDto dto);
    }
}
