using Microsoft.AspNetCore.Mvc;
using MentorShip.Models;
using MentorShip.Services;
using System.Threading.Tasks;

namespace MentorShip.Controllers
{
	[ApiController]
	[Route("application")]
	public class ApplicationController : ControllerBase
	{
		private readonly ApplicationService _applicationService;

		public ApplicationController(ApplicationService applicationService)
		{
			_applicationService = applicationService;
		}

		[HttpGet("getAllApplication")]
		public async Task<IActionResult> GetAllApplication()
		{
			var applications = await _applicationService.GetAllApplication();
			return Ok(new { data = applications });
		}

		[HttpGet("getApplicationById/{id}")]
		public async Task<IActionResult> GetApplicationById(string id)
		{
			var application = await _applicationService.GetApplicationById(id);
			return Ok(new { data = application });
		}

		[HttpPut("updateApplicationStatus/{id}")]
		public async Task<IActionResult> UpdateApplicationStatus(string id, [FromBody] ApplicationStatus status)
		{
			var application = await _applicationService.UpdateApplicationStatus(id, status);
			return Ok(new { data = application });
		}

		[HttpDelete("deleteApplication/{id}")]
		public async Task<IActionResult> DeleteApplication(string id)
		{
			await _applicationService.DeleteApplication(id);
			return Ok();
		}
	
		[HttpPost("createApplication")]
		public async Task<IActionResult> CreateApplication([FromBody] Application application)
		{
			try
			{
				await _applicationService.CreateApplication(application);
				return Ok(new { data = application });
			}
			catch (Exception ex)
			{
				return BadRequest(new { error = ex.Message });
			}
		}


	}
}
