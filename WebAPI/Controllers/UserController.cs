

using Application.Dtos;
using Application.Services.Authorization;
using Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtUtils _jwtUtils;

        public UserController(IAuthService authService, IJwtUtils jwtUtils)
        {
            _authService = authService;
            _jwtUtils = jwtUtils;
        }
        [HttpPost("register")]
        public ActionResult Register(UserDto userDto)
        {
            var hashed = _authService.HashPassword(userDto.Password);
            userDto.Password = hashed;
            return Ok(userDto);
        }

        [HttpPost("login")]
        public ActionResult Login(UserDto userDto)
        {
            var authenticated = _authService.VerifyPassword(userDto.Password, "changeItLater");
            return Ok(authenticated);
        }
        [HttpPost("token")]
        public ActionResult CreateToken(UserDto userDto)
        {
            return Ok(_jwtUtils.GenerateJwtToken(userDto));
        }

    }
}