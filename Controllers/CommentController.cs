using Microsoft.AspNetCore.Mvc;
using MentorShip.Models;
using MentorShip.Services;

namespace MentorShip.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly CommentService _commentService;
        private readonly MenteeService _menteeService;
        private readonly MentorService _mentorService;
        public CommentController(CommentService commentService, MenteeService menteeService, MentorService mentorService)
        {
            _commentService = commentService;
            _menteeService = menteeService;
            _mentorService = mentorService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Comment comment)
        {
            try
            {
                var mentee = await _menteeService.GetMenteeById(comment.MenteeId);
                var mentor = await _mentorService.GetMentorById(comment.MentorId);
                if (mentee == null || mentor == null)
                {
                    return BadRequest(new { error = "Mentee or Mentor not found" });
                }
                await _commentService.CreateComment(comment);
                return CreatedAtAction(nameof(Get), new { id = comment.Id }, comment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("get/{id}")]
        public async Task<Comment> Get(string id)
        {
            return await _commentService.GetCommentById(id);
        }

        [HttpGet("getall/{id}")]
        public async Task<List<Comment>> GetAll(string id)
        {
            return await _commentService.GetAllCommentByMentorId(id);
        }
    }
}