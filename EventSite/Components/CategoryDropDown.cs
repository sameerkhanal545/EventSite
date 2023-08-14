using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EventSite.Models;

namespace EventSite.Components
{
    public class CategoryDropDown : ViewComponent
    {
        private IRepository<Category> data { get; set; }
        public CategoryDropDown(IRepository<Category> rep) => data = rep;

        public IViewComponentResult Invoke(string selectedValue)
        {
            var categories = data.List(new QueryOptions<Category> {
                OrderBy = g => g.Name
            });
            
            var vm = new DropDownViewModel {
                SelectedValue = selectedValue,
                DefaultValue = EventsGridDTO.DefaultFilter,
                Items = categories.ToDictionary(g => g.CategoryId.ToString(), g => g.Name)
            };

            return View(SharedPath.Select, vm);
        }
    }
}
