using SportTracker.Shared.Models;

namespace SportTracker.Client.Services
{
    public interface ISportEventService
    {
        Task AddSportEvent(SportEventInput sportEventInput);
    }
}
