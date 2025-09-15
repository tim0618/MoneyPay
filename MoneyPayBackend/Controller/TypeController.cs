using Microsoft.AspNetCore.Mvc;
using MoneyPayBackend.IService;
using MoneyPayBackend.Request;

namespace MoneyPayBackend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeController : ControllerBase
    {
        private readonly ITypeService _typeService;
        public TypeController(ITypeService typeService)
        {
            _typeService = typeService;
        }
        [HttpPost("Index")]
        public IActionResult Index(TypeRequest typeRequest)
        {
            var types = _typeService.GetTypesByEmail(typeRequest);
            return Ok(types);
        }

    }
}