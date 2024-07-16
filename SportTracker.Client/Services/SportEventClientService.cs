using SportTracker.Client.Shared;
using SportTracker.Shared.Models;
using SportTracker.Shared.Services;

namespace SportTracker.Client.Services
{
    public class SportEventClientService(IHttpService httpService) : ISportEventService
    {
        private readonly IHttpService _httpService = httpService;

        public async Task AddSportEvent(SportEventInput sportEventInput)
        {
            await _httpService.Post($"api/sportevent", sportEventInput);
        }

        // todo get data for some nice visualisations
    }
}
