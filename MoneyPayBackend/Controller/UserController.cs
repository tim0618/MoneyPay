using Microsoft.AspNetCore.Mvc;
using MoneyPayBackend.IService;
using MoneyPayBackend.Request;

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

        [HttpPost("Login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            var result = _userService.Login(loginRequest);
            return Ok(result);
        }
    }
}