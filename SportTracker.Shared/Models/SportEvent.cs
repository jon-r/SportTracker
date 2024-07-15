namespace SportTracker.Shared.Models
{
    public class SportEvent
    {
        public int SportEventId { get; set; }
        public SportEventType Type { get; set; }
        public DateTime UploadTimestamp { get; set; } = default!;
        public int Laps {get; set;}
        public int TimeSeconds {get; set;}
    }
}