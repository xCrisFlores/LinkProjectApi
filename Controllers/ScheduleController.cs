using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;

namespace LinkprojectAPI.Controllers
{
    [ApiController]
    [Route("api/student/shchedule[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleServices _scheduleService;

        public ScheduleController(ScheduleServices scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedule(int code)
        {
            try
            {
                var schedule = await _scheduleService.FindAll(code);
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDay([FromBody] Schedule day)
        {
            if (day == null)
            {
                return BadRequest("Day is null.");
            }

            try
            {
                int created = await _scheduleService.Insert(day);
                if (created == 1)
                {
                    return Ok(created);
                }

                return StatusCode(500, "Failed to add day.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id, day}")]
        public async Task<IActionResult> UpdatePerson(int id, string day1, [FromBody] Schedule day)
        {
            if (day == null)
            {
                return BadRequest("Day data is incorrect or incomplete.");
            }

            try
            {
                await _scheduleService.Update(id, day1, day);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
