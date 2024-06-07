using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace ApiEstoqueASP.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<User, ReadUserDto>();
        }
    }
}
