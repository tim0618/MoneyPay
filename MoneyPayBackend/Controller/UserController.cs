using Microsoft.AspNetCore.Mvc;
using MoneyPayBackend.IService;
using MoneyPayBackend.Request;
using LoginRequest = MoneyPayBackend.Request.LoginRequest;

namespace MoneyPayBackend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterRequest registerRequest)
        {
            var retsult = _userService.Register(registerRequest);
            return Ok(retsult);
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            var result = _userService.Login(loginRequest);
            return Ok(result);
        }
    }
}