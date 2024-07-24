namespace SportTracker.Server.Models
{
    public class SportEvent
    {
        public int SportEventId { get; set; }
        public SportEventType EventType { get; set; }
        public DateTime CreatedAt { get; set; } = default!;
        public int Distance { get; set; }
        public int Time { get; set; }
    }
}
