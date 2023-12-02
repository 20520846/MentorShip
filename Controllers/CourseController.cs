using Microsoft.AspNetCore.Mvc;
using MentorShip.Models;
using MentorShip.Services;


namespace MentorShip.Controllers
{
    [ApiController]
    [Route("course")]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;
        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet("{id}")]
        public async Task<Course> Get(string id)
        {
            return await _courseService.GetCourseById(id);
        }
        [HttpGet]
        public async Task<List<Course>> GetList()
        {
            return await _courseService.GetListAsync();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Course course)
        {
            try
            {
                await _courseService.CreateAsync(course);
                return CreatedAtAction(nameof(Get), new { id = course.Id }, course);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(string id, string mentorId, decimal price,double ratingStar)
        {
            try
            {
                await _courseService.UpdateAsync(id, mentorId, price, ratingStar);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(string id)
        {
            try
            {
                await _courseService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }   
}
