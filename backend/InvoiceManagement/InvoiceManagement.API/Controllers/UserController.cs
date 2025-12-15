

using InvoiceManagement.API.Dtos.User;
using InvoiceManagement.API.Mappeurs;
using InvoiceManagement.API.Utils;
using InvoiceManagement.BLL.Services.Interfaces;
using InvoiceManagement.DL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly JwtUtils _jwtUtils;

        public UserController(IUserService userService, JwtUtils jwtUtils)
        {
            _userService = userService;
            _jwtUtils = jwtUtils;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterFormDto form)
        {
            _userService.Register(form.ToUser());
            return NoContent();
        }

        [HttpPost("login")]
        public ActionResult<UserTokenDto> Login([FromBody] LoginFormDto form)
        {
            User user = _userService.Login(form.Login, form.Password);
            UserTokenDto userToken = new UserTokenDto
            {
                User = user.ToUserDto(),
                Token = _jwtUtils.GenerateToken(user),
            };
            return Ok(userToken);
        }
    }
}
