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
                if (builder.CurrentRoute.PriceFilter == "under7")
                    Where = b => b.Price < 7;
                else if (builder.CurrentRoute.PriceFilter == "7to14")
                    Where = b => b.Price >= 7 && b.Price <= 14;
                else
                    Where = b => b.Price > 14;
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
