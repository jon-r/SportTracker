using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportTracker.Server.Models;
using SportTracker.Shared.Models;

namespace SportTracker.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SportEventController(ISportEventRepository sportEventRepository) : ControllerBase
    {
        private readonly ISportEventRepository _sportEventRepository = sportEventRepository;

        [HttpGet]
        public ActionResult GetEvents([FromQuery] SportEventType? eventType, int page)
        {
            return Ok(_sportEventRepository.GetEvents(eventType, page));
        }

        [HttpPost]
        public async Task<ActionResult> AddEventAsync([FromBody] SportEventInput sportEvent)
        {
            return Ok(await _sportEventRepository.AddEventAsync(sportEvent));
        }
    }
}
