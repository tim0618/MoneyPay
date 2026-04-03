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
        [HttpGet("GetMoneyTypes")]
        public IActionResult GetMoneyTypes([FromQuery] string? type = null)
        {
            try
            {
                var result = _typeService.GetMoneyTypes(userEmail, type);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetDeletedMoneyTypes")]
        public IActionResult GetDeletedMoneyTypes([FromQuery] string? type = null)
        {
            try
            {
                var result = _typeService.GetDeletedMoneyTypes(userEmail, type);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("CreateMoneyType")]
        public IActionResult CreateMoneyType(CreateMoneyTypeRequest createMoneyTypeRequest)
        {
            try
            {
                var result = _typeService.CreateMoneyType(userEmail, createMoneyTypeRequest);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("CreateSubMoneyType/{parentId}")]
        public IActionResult CreateSubMoneyType(int parentId, CreateSubMoneyTypeRequest createSubMoneyTypeRequest)
        {
            try
            {
                var result = _typeService.CreateSubMoneyType(userEmail, parentId, createSubMoneyTypeRequest);
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
        [HttpPut("UpdateSubMoneyType/{subCategoryId}")]
        public IActionResult UpdateSubMoneyType(int subCategoryId, UpdateSubMoneyTypeRequest updateSubMoneyTypeRequest)
        {
            try
            {
                var result = _typeService.UpdateSubMoneyType(userEmail, subCategoryId, updateSubMoneyTypeRequest);
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
        [HttpDelete("DeleteMoneyType/{typeId}")]
        public IActionResult DeleteMoneyType(int typeId)
        {
            try
            {
                var result = _typeService.DeleteMoneyType(userEmail, typeId);
                return result ? NoContent() : NotFound("Category not found");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("DeleteSubMoneyType/{subCategoryId}")]
        public IActionResult DeleteSubMoneyType(int subCategoryId)
        {
            try
            {
                var result = _typeService.DeleteSubMoneyType(userEmail, subCategoryId);
                return result ? NoContent() : NotFound("Subcategory not found");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("RestoreMoneyType/{typeId}")]
        public IActionResult RestoreMoneyType(int typeId)
        {
            try
            {
                var result = _typeService.RestoreMoneyType(userEmail, typeId);
                return result ? NoContent() : NotFound("Category not found");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("RestoreSubMoneyType/{subCategoryId}")]
        public IActionResult RestoreSubMoneyType(int subCategoryId)
        {
            try
            {
                var result = _typeService.RestoreSubMoneyType(userEmail, subCategoryId);
                return result ? NoContent() : NotFound("Subcategory not found");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
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
