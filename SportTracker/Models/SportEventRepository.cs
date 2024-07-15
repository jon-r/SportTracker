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
                    .SportEvents.Where(e => e.Type == eventType)
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

        public async Task<SportEvent> AddEventAsync(SportEvent sportEvent)
        {
            sportEvent.UploadTimestamp = DateTime.UtcNow;
            sportEvent.Type = (SportEventType)sportEvent.Type;

            var result = await _appDbContext.SportEvents.AddAsync(sportEvent);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
