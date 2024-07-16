using SportTracker.Shared.Models;

namespace SportTracker.Shared.Services
{
    public interface ISportEventService
    {
        Task AddSportEvent(SportEventInput sportEventInput);
    }
}
