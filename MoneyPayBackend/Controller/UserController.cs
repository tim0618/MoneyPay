using Microsoft.AspNetCore.Mvc;

namespace MoneyPayBackend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers()
        {
            int a = 0;
            return Ok(a);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            return Ok(new { message = $"User with id {id}" });
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] object userDto)
        {
            return CreatedAtAction(nameof(GetUserById), new { id = 1 }, userDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] object userDto)
        {
            return NoContent();
        }

    }
}