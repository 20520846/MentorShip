using Microsoft.AspNetCore.Mvc;
using MentorShip.Models;
using MentorShip.Services;

namespace MentorShip.Controllers
{
    [ApiController]
    [Route("api/mentor")]
    public class MentorController : ControllerBase
    {
        private readonly MentorService _mentorService;
        public MentorController(MentorService mentorService)
        {
            _mentorService = mentorService;
        }

        [HttpGet("get/{id}")]
        public async Task<Mentor> Get(string id)
        {
            return await _mentorService.GetMentorById(id);
        }

        [HttpGet("get")]

        public async Task<List<Mentor>> GetAllMentors()
        {
            return await _mentorService.GetAllMentors();

        }
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Mentor mentor)
        {
            try
            {
                await _mentorService.RegisterMentor(mentor);
                return Ok(new { data = mentor });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] string[]? skillIds, [FromQuery] int? minPrice, [FromQuery] int? maxPrice)
        {
            var mentors = await _mentorService.SearchMentor(name, skillIds, minPrice, maxPrice);
            return Ok(new { data = mentors });
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Mentor mentor)
        {
            try
            {
                var existingMentor = await _mentorService.GetMentorById(id);
                if (existingMentor == null)
                {
                    return NotFound();
                }

                mentor.Id = existingMentor.Id; // Ensure the ID is not changed
                var updatedMentor = await _mentorService.UpdateMentor(mentor);

                return Ok(updatedMentor); // Return the updated mentor
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpGet("getMentorSkills/{mentorId}")]
        public async Task<IActionResult> GetMentorSkills(string mentorId)
        {
            try
            {
                var skills = await _mentorService.GetSkillsByMentorId(mentorId);
                return Ok(new { data = skills });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("updateLockStatus/{mentorId}")]
        public async Task<IActionResult> UpdateLockStatus(string mentorId)
        {
            try
            {
                var mentor = await _mentorService.GetMentorById(mentorId);
                if (mentor == null)
                {
                    return NotFound();
                }

                mentor.IsLocked = !mentor.IsLocked;
                await _mentorService.UpdateMentor(mentor);

                return Ok(new { data = mentor });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}