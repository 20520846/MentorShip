// FieldController.cs
using Microsoft.AspNetCore.Mvc;
using MentorShip.Models;
using MentorShip.Services;
using System.Threading.Tasks;

namespace MentorShip.Controllers
{
    [ApiController]
    [Route("field")]
    public class FieldController : ControllerBase
    {
        private readonly FieldService _fieldService;

        public FieldController(FieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [HttpPost("createField")]
        public async Task<IActionResult> CreateField([FromBody] Field field)
        {
            try
            {
                await _fieldService.CreateField(field);
                return Ok(new { data = field });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("getAllFields")]
        public async Task<IActionResult> GetAllFields()
        {
            var fields = await _fieldService.GetAllFields();
            return Ok(new { data = fields });
        }

        [HttpGet("getFieldById/{id}")]
        public async Task<IActionResult> GetFieldById(string id)
        {
            var field = await _fieldService.GetFieldById(id);
            return Ok(new { data = field });
        }

        [HttpDelete("deleteField/{id}")]
        public async Task<IActionResult> DeleteField(string id)
        {
            await _fieldService.DeleteField(id);
            return Ok();
        }
 
    
    }
}
