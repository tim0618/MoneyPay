using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MoneyPayBackend.IService;
using MoneyPayBackend.Request;
using LoginRequest = MoneyPayBackend.Request.LoginRequest;

namespace MoneyPayBackend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private string userEmail => User.Claims.FirstOrDefault(c => c.Type == "userEmail")?.Value;
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

        [Authorize]
        [HttpGet("GetProfile")]
        public IActionResult GetProfile()
        {
            try
            {
                var result = _userService.GetProfile(userEmail);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("UpdateProfile")]
        public IActionResult UpdateProfile(UpdateUserProfileRequest updateUserProfileRequest)
        {
            try
            {
                var result = _userService.UpdateProfile(userEmail, updateUserProfileRequest);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            try
            {
                var result = _userService.ChangePassword(userEmail, changePasswordRequest);
                return result ? NoContent() : BadRequest("Change password failed");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
