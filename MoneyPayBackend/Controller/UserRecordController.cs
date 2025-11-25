using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyPayBackend.IService;

namespace MoneyPayBackend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRecordController : ControllerBase
    {
        private string userEmail => User.Claims.FirstOrDefault(e => e.Type == "userEmail")?.Value;

        private readonly IUserRecordService _userRecordService;
        public UserRecordController(IUserRecordService userRecordService)
        {
            _userRecordService = userRecordService;
        }

        [Authorize]
        [HttpGet("GetBookRecords")]
        public IActionResult GetBookRecords()
        {
            var result = _userRecordService.GetBookRecords(userEmail);
            return Ok(result);
        }
    }
}