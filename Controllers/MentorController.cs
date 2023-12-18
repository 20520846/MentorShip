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

        public async Task<IActionResult> GetAllMentors()
        {
            var mentors = await _mentorService.GetAllMentors();
            return Ok(new { data = mentors });
        }
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Mentor mentor)
        {
            try
            {
                await _mentorService.RegisterMentor(mentor);
                return CreatedAtAction(nameof(Get), new { id = mentor.Id }, mentor);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] List<string>? skillIds, [FromQuery] int? minPrice, [FromQuery] int? maxPrice)
        {
            var mentors = await _mentorService.SearchMentor(name, skillIds, minPrice, maxPrice);
            return Ok(new { data = mentors });
        }

    }
}