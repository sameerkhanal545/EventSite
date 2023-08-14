using Microsoft.AspNetCore.Mvc;
using EventSite.Models;

namespace EventSite.Controllers
{
    public class OrganizerController : Controller
    {
        private IRepository<Organizer> data { get; set; }
        public OrganizerController(IRepository<Organizer> rep) => data = rep;

        public IActionResult Index() => RedirectToAction("List");

        public ViewResult List(GridDTO vals)
        {
            string defaultSort = nameof(Organizer.OrganizerName);
            var builder = new GridBuilder(HttpContext.Session, vals, defaultSort);
            builder.SaveRouteSegments();

            var options = new QueryOptions<Organizer> {
                Includes = "EventOrganizers.Event",
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize,
                OrderByDirection = builder.CurrentRoute.SortDirection
            };
            if (builder.CurrentRoute.SortField.EqualsNoCase(defaultSort))
                options.OrderBy = a => a.OrganizerName;
           

            var vm = new GridViewModel<Organizer> {
                Items = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };

            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var organizer = data.Get(new QueryOptions<Organizer> {
                Includes = "EventOrganizers.Event",
                Where = a => a.OrganizerId == id
            });
            return View(organizer);
        }
    }
}