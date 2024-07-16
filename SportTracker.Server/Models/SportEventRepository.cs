using SportTracker.Shared.Data;
using SportTracker.Shared.Models;

namespace SportTracker.Server.Models
{
    public class SportEventRepository(AppDbContext appDbContext) : ISportEventRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public PagedResult<SportEvent> GetEvents(SportEventType? eventType, int page)
        {
            int pageSize = 5;

            if (eventType != null)
            {
                return _appDbContext
                    .SportEvents.Where(e => e.EventType == eventType)
                    .OrderBy(e => e.SportEventId)
                    .GetPaged(page, pageSize);
            }
            else
            {
                return _appDbContext
                    .SportEvents.OrderBy(e => e.SportEventId)
                    .GetPaged(page, pageSize);
            }
        }

        public async Task<SportEvent> AddEventAsync(SportEventInput sportEventInput)
        {
            var eventType = (SportEventType)sportEventInput.EventType;
            var sportEvent = new SportEvent
            {
                EventType = eventType,
                Time = sportEventInput.Time * 60,
                Distance = DistanceFromEventType(sportEventInput.Laps, eventType),
                CreatedAt = DateTime.UtcNow
            };

            var result = await _appDbContext.SportEvents.AddAsync(sportEvent);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        private static int DistanceFromEventType(int laps, SportEventType eventType)
        {
            switch (eventType)
            {
                case SportEventType.Swimming:
                    return laps * 20;
                default:
                    return laps * 10;
            }
        }
    }
}
