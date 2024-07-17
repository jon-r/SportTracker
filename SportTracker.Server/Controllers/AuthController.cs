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

        // todo have this return 40X or 30X rather than 500 error
        [HttpPost("login")]
        public ActionResult Login([FromBody] AuthRequest authRequest)
        {
            return Ok(_authRepository.Authenticate(authRequest));
        }
    }
}
