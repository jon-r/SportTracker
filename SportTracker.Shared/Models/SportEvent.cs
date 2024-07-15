using System.Text.Json;
using System.Text.Json.Serialization;

namespace SportTracker.Shared.Models
{
    public class SportEvent
    {
        [JsonPropertyName("id")]
        public int SportEventId { get; set; }
        public SportEventType EventType { get; set; }
        public DateTime CreatedAt { get; set; } = default!;
        public int Distance { get; set; }
        public int Time { get; set; }
    }
}
