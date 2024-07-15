namespace SportTracker.Shared.Models
{
    public class SportEventInput
    {
        public SportEventType EventType { get; set; }
        public int Minutes { get; set; }
        public int Laps { get; set; }
    }
}
