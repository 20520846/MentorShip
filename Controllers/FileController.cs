using MentorShip.Models;
using MentorShip.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorShip.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileService _fileService;

        public FileController(FileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<FileModel>> CreateFile(FileModel file)
        {
            try
            {
                var createdFile = await _fileService.CreateFile(file);
                return Ok(createdFile);
            }
            catch (System.Exception)
            {
                return BadRequest(new { error = "Create file failed" });
            }
        }

        [HttpGet("getByMentorId/{mentorId}")]
        public async Task<ActionResult<List<FileModel>>> GetFilesByMentorId(string mentorId)
        {
            try
            {
                var files = await _fileService.GetFilesByMentorId(mentorId);
                return Ok(files);
            }
            catch (System.Exception)
            {
                return BadRequest(new { error = "Get files failed" });
            }
        }

        [HttpGet("getById/{id}")]
        public async Task<ActionResult<FileModel>> GetFileById(string id)
        {
            try
            {
                var file = await _fileService.GetFileById(id);
                return Ok(file);
            }
            catch (System.Exception)
            {
                return BadRequest(new { error = "Get file failed" });
            }
        }
    }
}