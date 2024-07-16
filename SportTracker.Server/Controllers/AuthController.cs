using Microsoft.AspNetCore.Mvc;
using SportTracker.Server.Models;
using SportTracker.Shared.Models;

namespace SportTracker.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthRepository authRepository) : ControllerBase
    {
        private readonly IAuthRepository _authRepository = authRepository;

        [HttpPost("login")]
        public ActionResult Login([FromBody] AuthRequest authRequest)
        {
            return Ok(_authRepository.Authenticate(authRequest));
        }

        [HttpPost("setup")]
        public async Task<ActionResult> AddUserAsync([FromBody] User user)
        {
            return Ok(await _authRepository.AddUserAsync(user));
        }
    }
}
