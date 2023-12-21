using Microsoft.AspNetCore.Mvc;
using MentorShip.Models;
using MentorShip.Services;

namespace MentorShip.Controllers
{
    [ApiController]
    [Route("api/menteeExam")]
    public class MenteeExamController : ControllerBase
    {
        private readonly MenteeExamService _menteeExamService;
        private readonly AnswerService _answerService;

        public MenteeExamController(MenteeExamService menteeExamService, AnswerService answerService)
        {
            _menteeExamService = menteeExamService;
            _answerService = answerService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] MenteeExam menteeExam)
        {
            try
            {
                await _menteeExamService.CreateMenteeExam(menteeExam);
                return Ok(new { data = menteeExam });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("getByMenteeExamId/{menteeExamId}")]
        public async Task<MenteeExam> GetByMenteeExamId(string menteeExamId)
        {
            return await _menteeExamService.GetMenteeExamById(menteeExamId);
        }

        [HttpGet("getByMenteeId/{menteeId}")]
        public async Task<List<MenteeExam>> Get(string menteeId)
        {
            return await _menteeExamService.GetMenteeExamByMenteeId(menteeId);
        }

        [HttpGet("getByMentorId/{mentorId}")]
        public async Task<List<MenteeExam>> GetByMentorId(string mentorId)
        {
            return await _menteeExamService.GetMenteeExamByMentorId(mentorId);
        }
    }

    [ApiController]
    [Route("api/answer")]
    public class AnswerController : ControllerBase
    {
        private readonly MenteeExamService _menteeExamService;

        private readonly AnswerService _answerService;

        public AnswerController(MenteeExamService menteeExamService, AnswerService answerService)
        {
            _menteeExamService = menteeExamService;
            _answerService = answerService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Answer answer)
        {
            try
            {
                await _answerService.CreateAnswer(answer);
                await _menteeExamService.UpdateNumberAns(answer.MenteeExamId, 1);
                return Ok(new { data = answer });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("getById/{Id}")]
        public async Task<Answer> GetByMenteeExamId(string Id)
        {
            return await _answerService.GetAnswerById(Id);
        }

        [HttpGet("getByMenteeExamId/{menteeExamId}")]
        public async Task<List<Answer>> Get(string menteeExamId)
        {
            return await _answerService.GetAnswerByMenteeExamId(menteeExamId);
        }

        [HttpGet("getByQuestionId/{questionId}")]
        public async Task<Answer> GetByQuestionId(string questionId)
        {
            return await _answerService.GetAnswerByQuestionId(questionId);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Answer answer)
        {
            try
            {
                var answerData = await _answerService.GetAnswerById(id);
                if (answerData == null)
                {
                    return BadRequest(new { error = "Answer not found" });
                }
                else
                {
                    await _answerService.UpdateAnswer(answer);
                    return Ok(new { data = answer });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}