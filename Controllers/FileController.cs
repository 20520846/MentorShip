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
        private readonly FolderService _folderService;

        public FileController(FileService fileService, FolderService folderService)
        {
            _fileService = fileService;
            _folderService = folderService;
        }

        [HttpPost("create/{folderId}")]
        public async Task<ActionResult<FileModel>> CreateFile(FileModel file, string folderId)
        {
            try
            {
                var createdFile = await _fileService.CreateFile(file);

                if (folderId != null)
                {
                    var folderUpdate = await _folderService.AddFileToFolder(folderId, createdFile.Id);
                }

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

    [Route("api/folder")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        private readonly FolderService _folderService;

        public FolderController(FolderService folderService)
        {
            _folderService = folderService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<FolderModel>> CreateFolder(FolderModel folder)
        {
            try
            {
                var createdFolder = await _folderService.CreateFolder(folder);
                return Ok(createdFolder);
            }
            catch (System.Exception)
            {
                return BadRequest(new { error = "Create folder failed" });
            }
        }

        [HttpGet("getByMentorId/{mentorId}")]
        public async Task<ActionResult<List<FolderModel>>> GetFoldersByMentorId(string mentorId)
        {
            try
            {
                var folders = await _folderService.GetFoldersByMentorId(mentorId);
                return Ok(folders);
            }
            catch (System.Exception)
            {
                return BadRequest(new { error = "Get folders failed" });
            }
        }
    }
}