using SportTracker.Server.Data;

namespace SportTracker.Server.Models.SportEvents
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

        public SportEvent AddEvent(SportEventInput sportEventInput)
        {
            var eventType = sportEventInput.EventType;
            var sportEvent = new SportEvent
            {
                EventType = eventType,
                Time = sportEventInput.Time * 60,
                Distance = DistanceFromEventType(sportEventInput.Laps, eventType),
                CreatedAt = DateTime.UtcNow
            };

            var result = _appDbContext.SportEvents.Add(sportEvent);
            _appDbContext.SaveChanges();

            return result.Entity;
        }

        public void InsertEvent(SportEvent sportEvent)
        {
            _appDbContext.SportEvents.Add(sportEvent);
            _appDbContext.SaveChanges();
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
