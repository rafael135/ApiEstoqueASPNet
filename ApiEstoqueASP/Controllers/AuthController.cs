using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Models;
using ApiEstoqueASP.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiEstoqueASP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        private ITokenService _tokenService;
        private IMapper _mapper;

        public AuthController(IUserService userService, ITokenService tokenService, IMapper mapper)
        {
            this._userService = userService;
            this._tokenService = tokenService;
            this._mapper = mapper;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserDto userDto)
        {
            var user = await this._userService.Register(userDto);

            if(user == null)
            {
                return BadRequest();
            }

            string userToken = this._tokenService.GenerateToken(user);

            ReadUserDto readUserDto = this._mapper.Map<ReadUserDto>(user);

            readUserDto.Token = userToken;

            return CreatedAtAction(
                null,
                new { id = readUserDto.Id },
                readUserDto
            );
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            var user = await this._userService.Login(loginUserDto);

            if(user == null)
            {
                return Unauthorized();
            }

            string userToken = this._tokenService.GenerateToken(user);

            ReadUserDto readUserDto = this._mapper.Map<ReadUserDto>(user);

            readUserDto.Token = userToken;

            return Ok(readUserDto);
        }
    }
}
