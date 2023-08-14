using Newtonsoft.Json;

namespace EventSite.Models
{
    public class EventsGridDTO : GridDTO
    {
        [JsonIgnore]
        public const string DefaultFilter = "all";

        public string Organizer { get; set; } = DefaultFilter;
        public string Category { get; set; } = DefaultFilter;
        public string Price { get; set; } = DefaultFilter;
    }
}
