using System.Collections.Generic;

namespace EventSite.Models
{
    public class EventDTO
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public Dictionary<int, string> Organizers { get; set; }
        public string ImagePath { get; set; }

        public void Load(Event events)
        {
			EventId = events.EventId;
            Title = events.Title;
            Price = events.Price;
            ImagePath= events.ImagePath;
			Organizers = new Dictionary<int, string>();
            foreach (EventOrganizer ba in events.EventOrganizers) {
				Organizers.Add(ba.OrganizerId, ba.Organizer.OrganizerName);
            }
        }
    }

}
