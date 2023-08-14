﻿using System.Collections.Generic;
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
                    { "under7", "Under $7" },
                    { "7to14", "$7 to $14" },
                    { "over14", "Over $14" }
                }
            };

            return View(SharedPath.Select, vm);
        }
    }
}
