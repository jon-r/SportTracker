using Microsoft.AspNetCore.Mvc;
using SportTracker.Server.Models;
using SportTracker.Shared.Models;

namespace SportTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SportEventController(ISportEventRepository sportEventRepository) : ControllerBase {

        private readonly ISportEventRepository _sportEventRepository = sportEventRepository;
        
        [HttpGet]
        public ActionResult GetEvents()
        {
            return Ok("cool");
        }

        
        // todo add authorisation
        [HttpPost]
        public async Task<ActionResult> AddEvent(SportEvent sportEvent)
        {
            return Ok(await _sportEventRepository.AddEvent(sportEvent));
        }
    }
}