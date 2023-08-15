using System.Linq;

namespace EventSite.Models
{
    public class EventQueryOptions : QueryOptions<Event>
    {
        public void SortFilter(EventsGridBuilder builder)
        {
            if (builder.IsFilterByCategory) {
                Where = b => b.CategoryId == builder.CurrentRoute.CategoryFilter;
            }
            if (builder.IsFilterByPrice) {
                if (builder.CurrentRoute.PriceFilter == "under10")
                    Where = b => b.Price < 10;
                else if (builder.CurrentRoute.PriceFilter == "10to20")
                    Where = b => b.Price >= 10 && b.Price <= 20;
                else
                    Where = b => b.Price > 20;
            }
            if (builder.IsFilterByOrganizer) {
                int id = builder.CurrentRoute.OrganizerFilter.ToInt();
                if (id > 0)
                    Where = b => b.EventOrganizers.Any(ba => ba.Organizer.OrganizerId == id);
            }

            if (builder.IsSortByCategory) {
                OrderBy = b => b.Category.Name;
            }
            else if (builder.IsSortByPrice) {
                OrderBy = b => b.Price;
            }
            else  {
                OrderBy = b => b.Title;
            }
        }
    }
}
