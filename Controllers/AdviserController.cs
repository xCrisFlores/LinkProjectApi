using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/adviser[controller]")]
    public class AdviserController : ControllerBase
    {
        private readonly AdvisersService _adviserService;

        public AdviserController(AdvisersService advisersService)
        {
            _adviserService = advisersService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adviser>>> GetUsers()
        {
            try
            {
                var adviser = await _adviserService.FindAll();
                return Ok(adviser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Adviser>> GetUser(int Code)
        {
            try
            {
                var user = await _adviserService.FindOne(Code);
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
        public async Task<IActionResult> CreatePerson([FromBody] Adviser user)
        {
            if (user == null)
            {
                return BadRequest("User is null.");
            }

            try
            {
                int created = await _adviserService.Insert(user);
                if (created == 1)
                {
                    return CreatedAtAction(nameof(GetUser), new { id = user.AdviserCode }, user);
                }

                return StatusCode(500, "Failed to create person.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson([FromBody] Adviser user)
        {
            if (user == null)
            {
                return BadRequest("User data is incorrect or incomplete.");
            }

            try
            {
                var existingUser = await _adviserService.FindOne(user.AdviserCode);
                if (existingUser == null)
                {
                    return NotFound();
                }

                await _adviserService.Update(user.AdviserCode, user);
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
                var user = await _adviserService.FindOne(Code);
                if (user == null)
                {
                    return NotFound();
                }

                await _adviserService.Delete(Code);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
