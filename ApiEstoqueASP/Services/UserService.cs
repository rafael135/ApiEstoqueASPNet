using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Models;
using ApiEstoqueASP.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace ApiEstoqueASP.Services;

public class UserService : IUserService
{
    private IMapper _mapper;
    private UserManager<User> _userManager;
    private SignInManager<User> _signInManager;

    public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        this._mapper = mapper;
        this._userManager = userManager;
        this._signInManager = signInManager;
    }

    public async Task<User?> Register(CreateUserDto dto)
    {
        User user = this._mapper.Map<User>(dto);

        // Checa se o email existe
        User? emailExists = _userManager.Users.Where(usr => usr.NormalizedEmail == user.Email.ToUpper()).FirstOrDefault();

        if (emailExists != null)
        {
            return null;
        }

        IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

        await _signInManager.UserManager.AddClaimAsync(user, new System.Security.Claims.Claim("Role", "User"));


        if(result.Succeeded == false)
        {
            return null;
        }

        // Adiciono o usuário registrado ao role "User"
        IdentityResult res = await this._userManager.AddToRoleAsync(user, "User");

        if(res.Succeeded == false)
        {
            return null;
        }

        return user;
    }

    public async Task<User?> Login(LoginUserDto dto)
    {
        User user = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(user => user.NormalizedEmail == dto.Email.ToUpper());

        if (user == null)
        {
            return null;
        }

        SignInResult result = await this._signInManager.PasswordSignInAsync(user, dto.Password, false, false);

        if(result.Succeeded == false)
        {
            return null;
        }

        return user;
    }
}
