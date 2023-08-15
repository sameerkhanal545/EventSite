using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;

namespace EventSite.Models
{
    public class EventsGridBuilder : GridBuilder
    {
        public EventsGridBuilder(ISession sess) : base(sess) { }

        public EventsGridBuilder(ISession sess, EventsGridDTO values, 
            string defaultSortField) : base(sess, values, defaultSortField)
        {
            bool isInitial = values.Category.IndexOf(FilterPrefix.Category) == -1;
            routes.OrganizerFilter = (isInitial) ? FilterPrefix.Organizer + values.Organizer : values.Organizer;
            routes.CategoryFilter = (isInitial) ? FilterPrefix.Category + values.Organizer : values.Category;
            routes.PriceFilter = (isInitial) ? FilterPrefix.Price + values.Price : values.Price;
        }

        public void LoadFilterSegments(string[] filter, Organizer organizer)
        {
            if (organizer == null) { 
                routes.OrganizerFilter = FilterPrefix.Organizer + filter[0];
            } else {
                routes.OrganizerFilter = FilterPrefix.Organizer + filter[0]
                    + "-" + filter[0].Slug();
            }
            routes.CategoryFilter = FilterPrefix.Category + filter[1];
            routes.PriceFilter = FilterPrefix.Price + filter[2];
        }
        public void ClearFilterSegments() => routes.ClearFilters();

		string def = EventsGridDTO.DefaultFilter;   
        public bool IsFilterByOrganizer => routes.OrganizerFilter != def;
        public bool IsFilterByCategory => routes.CategoryFilter != def;
        public bool IsFilterByPrice => routes.PriceFilter != def;

        public bool IsSortByCategory =>
            routes.SortField.EqualsNoCase(nameof(Category));
        public bool IsSortByPrice =>
            routes.SortField.EqualsNoCase(nameof(Event.Price));
    }
}
