using Microsoft.AspNetCore.Mvc;
using MentorShip.Models;
using MentorShip.Services;
using System.Threading.Tasks;

namespace MentorShip.Controllers
{
    [ApiController]
    [Route("skill")]
    public class SkillController : ControllerBase
    {
        private readonly SkillService _skillService;

        public SkillController(SkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpPost("createSkill")]
        public async Task<IActionResult> CreateSkill([FromBody] Skill skill)
        {
            try
            {
                await _skillService.CreateSkill(skill);
                return Ok(new { data = skill });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("getAllSkills")]
        public async Task<IActionResult> GetAllSkills()
        {
            var skills = await _skillService.GetAllSkills();
            return Ok(new { data = skills });
        }

        [HttpGet("getSkillById/{id}")]
        public async Task<IActionResult> GetSkillById(string id)
        {
            var skill = await _skillService.GetSkillById(id);
            return Ok(new { data = skill });
        }

        [HttpDelete("deleteSkill/{id}")]
        public async Task<IActionResult> DeleteSkill(string id)
        {
            await _skillService.DeleteSkill(id);
            return Ok();
        }
    }
}
