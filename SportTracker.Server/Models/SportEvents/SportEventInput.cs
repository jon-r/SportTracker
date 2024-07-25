namespace SportTracker.Server.Models.SportEvents
{
    public class SportEventInput
    {
        public SportEventType EventType { get; set; }
        public int Time { get; set; } = 0;
        public int Laps { get; set; } = 0;
    }
}
