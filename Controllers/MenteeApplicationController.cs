using Microsoft.AspNetCore.Mvc;
using MentorShip.Models;
using MentorShip.Services;
using System.Threading.Tasks;

namespace MentorShip.Controllers
{
    [ApiController]
    [Route("menteeApplication")]
    public class MenteeApplicationController : ControllerBase
    {
        private readonly MenteeApplicationService _menteeApplicationService;

        public MenteeApplicationController(MenteeApplicationService menteeApplicationService)
        {
            _menteeApplicationService = menteeApplicationService;
        }

        [HttpGet("getAllMenteeApplication")]
        public async Task<IActionResult> GetAllMenteeApplication()
        {
            var applications = await _menteeApplicationService.GetAllMenteeApplication();
            return Ok(new { data = applications });
        }

        [HttpGet("getMenteeApplicationByMenteeId/{menteeId}")]
        public async Task<IActionResult> GetMenteeApplicationByMenteeId(string menteeId)
        {
            var application = await _menteeApplicationService.GetMenteeApplicationByMenteeId(menteeId);
            return Ok(new { data = application });
        }

        [HttpGet("getMenteeApplicationByMentorId/{mentorId}")]
        public async Task<IActionResult> GetMenteeApplicationByMentorId(string mentorId)
        {
            var application = await _menteeApplicationService.GetMenteeApplicationByMentorId(mentorId);
            return Ok(new { data = application });
        }

        [HttpPut("updateMenteeApplicationStatus/{id}")]
        public async Task<IActionResult> UpdateMenteeApplicationStatus(string id, [FromBody] ApprovalStatus status)
        {
            var application = await _menteeApplicationService.UpdateMenteeApplicationStatus(id, status);
            return Ok(new { data = application });
        }

        [HttpDelete("deleteMenteeApplication/{id}")]
        public async Task<IActionResult> DeleteMenteeApplication(string id)
        {
            await _menteeApplicationService.DeleteMenteeApplication(id);
            return Ok();
        }

        [HttpPost("createMenteeApplication")]
        public async Task<IActionResult> CreateMenteeApplication([FromBody] MenteeApplicationModel menteeApplication)
        {
            try
            {
                await _menteeApplicationService.CreateMenteeApplication(menteeApplication);
                return Ok(new { data = menteeApplication });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
