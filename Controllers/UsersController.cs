using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;

namespace LinkprojectAPI.Controllers
{
    [ApiController]
    [Route("api/user[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UsersService _userService;

        public UserController(UsersService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var user = await _userService.FindAll();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int Code)
        {
            try
            {
                var user = await _userService.FindOne(Code);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User is null.");
            }

            try
            {
                int created = await _userService.Insert(user);
                if (created == 1)
                {
                    return CreatedAtAction(nameof(GetUser), new { id = user.Code }, user);
                }

                return StatusCode(500, "Failed to create person.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is incorrect or incomplete.");
            }

            try
            {
                var existingUser = await _userService.FindOne(user.Code);
                if (existingUser == null)
                {
                    return NotFound();
                }

                await _userService.Update(user.Code, user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int Code)
        {
            try
            {
                var user = await _userService.FindOne(Code);
                if (user == null)
                {
                    return NotFound();
                }

                await _userService.Delete(Code);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
