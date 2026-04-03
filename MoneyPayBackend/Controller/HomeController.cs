using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyPayBackend.IService;
using MoneyPayBackend.Request;

namespace MoneyPayBackend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private string userEmail => User.Claims.FirstOrDefault(c => c.Type == "userEmail")?.Value;

        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [Authorize]
        [HttpGet("GetSummary")]
        public IActionResult GetSummary([FromQuery] GetHomeSummaryRequest getHomeSummaryRequest)
        {
            var result = _homeService.GetSummary(userEmail, getHomeSummaryRequest);
            return Ok(result);
        }
    }
}
