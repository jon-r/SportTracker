namespace SportTracker.Shared.Models
{
    public class SportEventInput
    {
        public SportEventType EventType { get; set; }
        public int Time { get; set; } = 0;
        public int Laps { get; set; } = 0;
    }
}
