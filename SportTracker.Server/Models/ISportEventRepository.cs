using SportTracker.Server.Data;
using SportTracker.Shared.Models;

namespace SportTracker.Server.Models
{
    public interface ISportEventRepository
    {
        PagedResult<SportEvent> GetEvents(SportEventType? eventType, int page);
        Task<SportEvent> AddEventAsync(SportEventInput sportEvent);
    }
}
