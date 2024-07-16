using SportTracker.Shared.Models;
using SportTracker.Shared.Services;

namespace SportTracker.Server.Services
{
    public class SportEventServerService() : ISportEventService
    {
        public async Task AddSportEvent(SportEventInput sportEventInput)
        {
            // stubbed out, wont submit on serverside
            await Task.CompletedTask;
        }

        // todo get data for some nice visualisations
    }
}
