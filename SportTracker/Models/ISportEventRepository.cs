using SportTracker.Shared.Models;

namespace SportTracker.Server.Models
{
    public interface ISportEventRepository
    {
        // todo paged response
        Task<SportEvent> AddEventAsync(SportEvent sportEvent);
    }
}