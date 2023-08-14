using Microsoft.AspNetCore.Mvc;
using EventSite.Models;

namespace EventSite.Controllers
{
    public class EventController : Controller
    {
        private IRepository<Event> data { get; set; }
        public EventController(IRepository<Event> rep) => data = rep;

        public RedirectToActionResult Index() => RedirectToAction("List");

        public ViewResult List(EventsGridDTO values)
        {
            var builder = new EventsGridBuilder(HttpContext.Session, values, 
                defaultSortField: nameof(Event.Title));

            var options = new EventQueryOptions {
                Includes = "EventOrganizers.Organizer, Category",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };
            options.SortFilter(builder);

            var vm = new GridViewModel<Event> {
                Items = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };

            return View(vm);
        }

        public ViewResult Details(int id)
        {
            var evt = data.Get(new QueryOptions<Event> {
                Includes = "EventOrganizers.Organizer, Category",
                Where = b => b.EventId == id
            });
            return View(evt);
        }

        [HttpPost]
        public RedirectToActionResult Filter([FromServices]IRepository<Organizer> data, 
            string[] filter, bool clear = false)
        {
            var builder = new EventsGridBuilder(HttpContext.Session);

            if (clear) {
                builder.ClearFilterSegments();
            }
            else {
                var organizer = data.Get(filter[0].ToInt());
                builder.LoadFilterSegments(filter, organizer);
            }

            builder.SaveRouteSegments();
            return RedirectToAction("List", builder.CurrentRoute);
        }
    }   
}