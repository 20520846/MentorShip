using Microsoft.AspNetCore.Mvc;
using MentorShip.Models;
using MentorShip.Services;
using System;
using System.Threading.Tasks;

namespace MentorShip.Controllers
{
    [ApiController]
    [Route("learningProgress")]
    public class LearningProgressController : ControllerBase
    {
        private readonly LearningProgressService _learningProgressService;

        public LearningProgressController(LearningProgressService learningProgressService)
        {
            _learningProgressService = learningProgressService;
        }

        [HttpPost("createLearningProgress")]
        public async Task<IActionResult> CreateLearningProgress([FromBody] MenteeApplicationModel application)
        {
            var endDate = DateTime.Now.AddDays(application.Plan.Weeks * 7);
            var learningProgress = new LearningProgress
            {
                ApplicationId = application.Id,
                StartDate = DateTime.Now,
                EndDate = endDate,
                CallTimesLeft = application.Plan.CallTimes,
                AdditionalCallTimes = 0,
                MentorId = application.MentorId,
                MenteeId = application.MenteeProfile.Id
            };
            await _learningProgressService.CreateLearningProgress(learningProgress);
            return Ok(new { data = learningProgress });
        }

        [HttpPut("updateLearningProgress/{id}")]
        public async Task<IActionResult> UpdateLearningProgress(string id, [FromBody] LearningProgress updatedProgress)
        {
            var learningProgress = await _learningProgressService.UpdateLearningProgress(id, updatedProgress);
            return Ok(new { data = learningProgress });
        }


        [HttpGet("getLearningProgressByMenteeId/{menteeId}")]
        public async Task<IActionResult> GetLearningProgressByMenteeId(string menteeId)
        {
            var learningProgresses = await _learningProgressService.GetLearningProgressByMenteeId(menteeId);
            return Ok(new { data = learningProgresses });
        }

        [HttpGet("getLearningProgressByMentorId/{mentorId}/{year?}")]
        public async Task<IActionResult> GetLearningProgressByMentorId(string mentorId, int? year)
        {
            var learningProgresses = await _learningProgressService.GetLearningProgressByMentorId(mentorId, year);
            return Ok(new { data = learningProgresses });
        }

        [HttpGet("getLearningProgressByApplicationId/{applicationId}")]
        public async Task<IActionResult> GetLearningProgressByApplicationId(string applicationId)
        {
            var learningProgress = await _learningProgressService.GetLearningProgressByApplicationId(applicationId);
            if (learningProgress == null)
            {
                return NotFound();
            }
            return Ok(new { data = learningProgress });
        }
    }

    [ApiController]
    [Route("learningTestProgress")]
    public class LearningTestProgressController : ControllerBase
    {
        private readonly LearningTestProgressService _learningTestProgressService;

        public LearningTestProgressController(LearningTestProgressService learningTestProgressService)
        {
            _learningTestProgressService = learningTestProgressService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateLearningTestProgress([FromBody] MenteeApplicationModel application)
        {
            var learningTestProgress = new LearningTestProgress
            {
                ApplicationId = application.Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now + TimeSpan.FromDays(7),
                CallTimesLeft = 1,
                MentorId = application.MentorId,
                MenteeId = application.MenteeProfile.Id
            };
            await _learningTestProgressService.CreateLearningTestProgress(learningTestProgress);
            return Ok(new { data = learningTestProgress });
        }

        [HttpGet("getLearningTestProgressByMenteeId/{menteeId}")]
        public async Task<IActionResult> GetLearningTestProgressByMenteeId(string menteeId)
        {
            var learningTestProgresses = await _learningTestProgressService.GetLearningTestProgressByMenteeId(menteeId);
            return Ok(new { data = learningTestProgresses });
        }

    }
}
