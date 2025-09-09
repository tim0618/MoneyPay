using Microsoft.AspNetCore.Mvc;
using MoneyPayBackend.IService;

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

        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var a = _userService.GetUserById(id);
            return Ok(new { message = $"User with id {id}" });
        }
    }
}