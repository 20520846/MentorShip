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
        private readonly LearningProgressService _learningProgressService;

        public MenteeApplicationController(MenteeApplicationService menteeApplicationService, LearningProgressService learningProgressService)
        {
            _menteeApplicationService = menteeApplicationService;
            _learningProgressService = learningProgressService;
        }

        [HttpGet("getAllMenteeApplication")]
        public async Task<IActionResult> GetAllMenteeApplication()
        {
            var applications = await _menteeApplicationService.GetAllMenteeApplication();
            return Ok(new { data = applications });
        }

        [HttpGet("getMenteeApplicationById/{id}")]
        public async Task<IActionResult> GetMenteeApplicationById(string id)
        {
            var application = await _menteeApplicationService.GetMenteeApplicationById(id);
            return Ok(new { data = application });
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

        [HttpPost("checkRegistration")]
        public async Task<IActionResult> CheckMenteeRegistration([FromBody] MenteeMentorIds ids)
        {
            var menteeApplication = await _menteeApplicationService.GetMenteeApplicationByMenteeIdAndMentorId(ids.MenteeId, ids.MentorId);
            if (menteeApplication == null)
            {
                return Ok(new { CanProceed = true });
            }

            var status = menteeApplication.Status;

            if (status == ApprovalStatus.Rejected)
            {
                return Ok(new { CanProceed = true });
            }
            else if (status == ApprovalStatus.Pending)
            {
                return Ok(new { CanProceed = false });
            }
            else // status == ApprovalStatus.Approved
            {
                var learningProgress = await _learningProgressService.GetLearningProgressByApplicationId(menteeApplication.Id);
                if (learningProgress == null)
                {
                    return Ok(new { CanProceed = false });
                }

                var StartDate = learningProgress.StartDate;
                var weeks = menteeApplication.Plan.Weeks;
                var expiryDate = StartDate.AddDays(weeks * 7);

                if (DateTime.Now > expiryDate)
                {
                    return Ok(new { CanProceed = true });
                }
                else
                {
                    return Ok(new { CanProceed = false });
                }
            }
        }

    }
}
public class MenteeMentorIds
{
    public string MenteeId { get; set; }
    public string MentorId { get; set; }
}