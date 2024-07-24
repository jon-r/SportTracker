using SportTracker.Server.Data;

namespace SportTracker.Server.Models
{
    public interface ISportEventRepository
    {
        PagedResult<SportEvent> GetEvents(SportEventType? eventType, int page);
        SportEvent AddEvent(SportEventInput sportEvent);
        void InsertEvent(SportEvent sportEvent);
    }
}
