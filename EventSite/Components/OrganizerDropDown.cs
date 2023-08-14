using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EventSite.Models;

namespace EventSite.Components
{
    public class OrganizerDropDown : ViewComponent
    {
        private IRepository<Organizer> data { get; set; }
        public OrganizerDropDown(IRepository<Organizer> rep) => data = rep;

        public IViewComponentResult Invoke(string selectedValue)
        {
            var organizers = data.List(new QueryOptions<Organizer> {
                OrderBy = a => a.OrganizerName
			});
            
            var vm = new DropDownViewModel {
                SelectedValue = selectedValue,
                DefaultValue = EventsGridDTO.DefaultFilter,
                Items = organizers.ToDictionary(
                    a => a.OrganizerId.ToString(), a => a.OrganizerName)
            };

            return View(SharedPath.Select, vm);
        }
    }
}
