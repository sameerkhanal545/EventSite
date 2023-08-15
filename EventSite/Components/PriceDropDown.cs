using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EventSite.Models;

namespace EventSite.Components
{
    public class PriceDropDown : ViewComponent
    {
        public IViewComponentResult Invoke(string selectedValue)
        {
            var vm = new DropDownViewModel {
                SelectedValue = selectedValue,
                DefaultValue = EventsGridDTO.DefaultFilter,
                Items = new Dictionary<string, string> {
                    { "under10", "Under $10" },
                    { "10to20", "$10 to $20" },
                    { "over20", "Above $20" }
                }
            };

            return View(SharedPath.Select, vm);
        }
    }
}
