using Microsoft.AspNetCore.Mvc;
using MentorShip.Models;
using MentorShip.Services;

namespace MentorShip.Controllers
{
    [ApiController]
    [Route("api/menteeFile")]
    public class MenteeFileController : ControllerBase
    {
        private readonly MenteeFileService _menteeFileService;
        private readonly FileService _fileService;

        public MenteeFileController(MenteeFileService menteeFileService, FileService fileService)
        {
            _menteeFileService = menteeFileService;
            _fileService = fileService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] MenteeFile menteeFile)
        {
            try
            {
                await _menteeFileService.CreateMenteeFile(menteeFile);
                return Ok(new { data = menteeFile });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("getByMenteeFileId/{menteeFileId}")]
        public async Task<MenteeFile> GetByMenteeFileId(string menteeFileId)
        {
            return await _menteeFileService.GetMenteeFileById(menteeFileId);
        }

        [HttpGet("getByMenteeId/{menteeId}")]
        public async Task<List<MenteeFile>> Get(string menteeId)
        {
            return await _menteeFileService.GetMenteeFileByMenteeId(menteeId);
        }

        [HttpGet("getByMentorId/{mentorId}")]
        public async Task<List<MenteeFile>> GetByMentorId(string mentorId)
        {
            return await _menteeFileService.GetMenteeFileByMentorId(mentorId);
        }

        [HttpPut("updateFileId/{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] string fileId)
        {
            try
            {
                var menteeFile = await _menteeFileService.GetMenteeFileById(id);
                if (menteeFile == null)
                {
                    return BadRequest(new { error = "MenteeFile not found" });
                }
                menteeFile.FileId = fileId;
                await _menteeFileService.CreateMenteeFile(menteeFile);
                return Ok(new { data = menteeFile });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}