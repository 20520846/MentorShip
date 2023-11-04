using Microsoft.AspNetCore.Mvc;
using MentorShip.Models;
using MentorShip.Services;


namespace MentorShip.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
       

        public UserController(UserService userService )
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                await _userService.Register(user);
                return Ok(new { data = user });
            }
            catch (Exception ex)
            {
          
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var findUser = await _userService.Login(user.Email, user.Password);

            if (findUser == null)
            {
                return Unauthorized();
            }

            return Ok(new { data = findUser });
        }
    }
}
