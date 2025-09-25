using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyPayBackend.IService;
using MoneyPayBackend.Request;

namespace MoneyPayBackend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoneyTypeController : ControllerBase
    {
        // 這個因為版本或其他問題無法使用，要完整的路徑
        private string userEmailFullPath => User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
        private string userEmail => User.Claims.FirstOrDefault(c => c.Type == "userEmail")?.Value;


        private readonly ITypeService _typeService;
        public MoneyTypeController(ITypeService typeService)
        {
            _typeService = typeService;
        }

        [Authorize]
        [HttpGet("GetMoneyTypesSum")]
        public IActionResult GetMoneyTypesSum()
        {
            var types = _typeService.GetMoneyTypesSum(userEmail);
            return Ok(types);
        }

        [Authorize]
        [HttpGet("GetTypeRemark/{typeId}")]
        public IActionResult GetTypeRemarkById(int typeId)
        {
            var types = _typeService.GetTypeRemarkById(typeId);
            return Ok(types);
        }

        [Authorize]
        [HttpPost("AddTypePay")]
        public IActionResult AddTypePay(AddTypePayRequest addTypePay)
        {
            if (_typeService.AddTypePay(addTypePay)) return Ok();
            return BadRequest("AddTypePay Error");
        }

        [Authorize]
        [HttpPut("UpdateTypePay")]
        public IActionResult UpdateTypePay(UpdateTypePayRequest updateTypePay)
        {
            if (_typeService.UpdateTypePay(updateTypePay)) return Ok();
            return BadRequest("UpdateTypePay Error");
        }

        [Authorize]
        [HttpDelete("DeleteTypePay")]
        public IActionResult DeleteTypePay(DeleteTypePayRequest deleteTypePay)
        {
            if (_typeService.DeleteTypePay(deleteTypePay)) return Ok();
            return BadRequest("DeleteTypePay Error");
        }
    }
}