using Microsoft.AspNetCore.Mvc;
using MentorShip.Models;
using MentorShip.Services;

namespace MentorShip.Controllers
{
    [ApiController]
    [Route("api/plan")]
    public class PlanController : ControllerBase
    {
        private readonly PlanService _planService;
        private readonly MentorService _mentorService;
        public PlanController(PlanService planService, MentorService mentorService)
        {
            _planService = planService;
            _mentorService = mentorService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Plan plan)
        {
            try
            {
                var mentor = await _mentorService.GetMentorById(plan.MentorId);
                if (mentor == null)
                {
                    return BadRequest(new { error = "Mentor not found" });
                }
                await _planService.CreatePlan(plan);
                return CreatedAtAction(nameof(Get), new { id = plan.Id }, plan);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("get/{id}")]
        public async Task<Plan> Get(string id)
        {
            return await _planService.GetPlanById(id);
        }

        [HttpGet("getall/{id}")]
        public async Task<List<Plan>> GetAll(string id)
        {
            return await _planService.GetAllPlanByMentorId(id);
        }
    }
}