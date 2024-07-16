using SportTracker.Client.Shared;
using SportTracker.Shared.Models;

namespace SportTracker.Client.Services
{
    public class SportEventService(IHttpService httpService) : ISportEventService
    {
        private readonly IHttpService _httpService = httpService;

        public async Task AddSportEvent(SportEventInput sportEventInput)
        {
            await _httpService.Post($"api/sportevent", sportEventInput);
        }

        // todo get data for some nice visualisations
    }
}
