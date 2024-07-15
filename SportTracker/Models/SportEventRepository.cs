using SportTracker.Shared.Models;

namespace SportTracker.Server.Models
{
    public class SportEventRepository(AppDbContext appDbContext) : ISportEventRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<SportEvent> AddEventAsync(SportEvent sportEvent)
        {
            sportEvent.UploadTimestamp = DateTime.UtcNow;
            var result = await _appDbContext.SportEvents.AddAsync(sportEvent);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}