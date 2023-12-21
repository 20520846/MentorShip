using Microsoft.AspNetCore.Mvc;
using MentorShip.Models;
using MentorShip.Services;

namespace MentorShip.Controllers
{
    [ApiController]
    [Route("api/exam")]
    public class ExamController : ControllerBase
    {
        private readonly ExamService _examService;
        private readonly QuestionService _questionService;

        public ExamController(ExamService examService, QuestionService questionService)
        {
            _examService = examService;
            _questionService = questionService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Exam exam)
        {
            try
            {
                await _examService.CreateExam(exam);
                return Ok(new { data = exam });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("getByExamId/{id}")]
        public async Task<Exam> GetByExamId(string id)
        {
            return await _examService.GetExamById(id);
        }

        [HttpGet("getByMentorId/{mentorId}")]
        public async Task<List<Exam>> GetByMentorId(string mentorId)
        {
            return await _examService.GetExamByMentorId(mentorId);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var exam = await _examService.GetExamById(id);
                if (exam == null)
                {
                    return BadRequest(new { error = "Exam not found" });
                }
                else
                {
                    await _examService.DeleteExamById(id);
                    await _questionService.DeleteQuestionByExamId(id);
                    return Ok(new { data = "Delete successfully" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }

    [ApiController]
    [Route("api/question")]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService _questionService;
        private readonly ExamService _examService;

        public QuestionController(QuestionService questionService, ExamService examService)
        {
            _questionService = questionService;
            _examService = examService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Question question)
        {
            try
            {
                var exam = await _examService.GetExamById(question.ExamId);
                if (exam == null)
                {
                    return BadRequest(new { error = "Exam not found" });
                }
                else
                {
                    await _questionService.CreateQuestion(question);
                    await _examService.UpdateNumberQues(question.ExamId, 1);
                    return Ok(new { data = question });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("get/{examId}")]
        public async Task<List<Question>> Get(string examId)
        {
            return await _questionService.GetQuestionsByExamId(examId);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var question = await _questionService.GetQuestionById(id);
                if (question == null)
                {
                    return BadRequest(new { error = "Question not found" });
                }
                else
                {
                    await _questionService.DeleteQuestionById(id);
                    await _examService.UpdateNumberQues(question.ExamId, -1);
                    // return Ok(new { data = question });
                    return Ok(new { data = "Delete successfully" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}