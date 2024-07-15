using SportTracker.Shared.Data;
using SportTracker.Shared.Models;

namespace SportTracker.Server.Models
{
    public interface ISportEventRepository
    {
        // todo paged response
        PagedResult<SportEvent> GetEvents(SportEventType? eventType, int page);
        Task<SportEvent> AddEventAsync(SportEvent sportEvent);
    }
}
