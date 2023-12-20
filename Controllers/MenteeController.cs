using Microsoft.AspNetCore.Mvc;
using MentorShip.Models;
using MentorShip.Services;

namespace MentorShip.Controllers
{
    [ApiController]
    [Route("api/mentee")]
    public class MenteeController : ControllerBase
    {
        private readonly MenteeService _menteeService;
        public MenteeController(MenteeService menteeService)
        {
            _menteeService = menteeService;
        }

        [HttpGet("get/{id}")]
        public async Task<Mentee> Get(string id)
        {
            return await _menteeService.GetMenteeById(id);
        }



        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Mentee mentee)
        {
            try
            {
                await _menteeService.RegisterMentee(mentee);
                return CreatedAtAction(nameof(Get), new { id = mentee.Id }, mentee);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}