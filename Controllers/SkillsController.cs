using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;

namespace LinkprojectAPI.Controllers
{
    [ApiController]
    [Route("api/student/skills[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly SkillsService _service;

        public SkillsController(SkillsService SkillService)
        {
            _service = SkillService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
        {
            try
            {
                var skills = await _service.FindAll();
                return Ok(skills);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetSkill(int Code)
        {
            try
            {
                var skill = await _service.FindOne(Code);
                if (skill == null)
                {
                    return NotFound();
                }
                return Ok(skill);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkill([FromBody] Skill skill)
        {
            if (skill == null)
            {
                return BadRequest("Skill is null.");
            }

            try
            {
                int created = await _service.Insert(skill);
                if (created == 1)
                {
                    return CreatedAtAction(nameof(GetSkill), new { id = skill.StudentCode }, skill);
                }

                return StatusCode(500, "Failed to add skill.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson([FromBody] Skill skill)
        {
            if (skill == null)
            {
                return BadRequest("Skill data is incorrect or incomplete.");
            }

            try
            {
                var existingSkill = await _service.FindOne(skill.StudentCode);
                if (existingSkill == null)
                {
                    return NotFound();
                }

                await _service.Update(skill.StudentCode, skill);
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
                var skill = await _service.FindOne(Code);
                if (skill == null)
                {
                    return NotFound();
                }

                await _service.Delete(Code);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
