using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EventSite.Models;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace EventSite.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class EventController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IEventSiteUnitOfWork data;

        public EventController(IWebHostEnvironment environment, EventSiteContext ctx)
        {
            hostingEnvironment = environment;
            data = new EventSiteUnitOfWork(ctx);
        }
        public ViewResult Index()
        {
            var search = new SearchData(TempData);
            search.Clear();

            var options = new EventQueryOptions
            {
                Includes = "EventOrganizers.Organizer, Category",
                OrderByDirection = "asc",
                PageNumber = 1,
                PageSize = 5
            };
            var latestEvents = data.Events
          .List(options)
          .ToList();

            var SearchViewModel = new SearchViewModel
            {
                Events = latestEvents,
            };

            return View(SearchViewModel);
        }

        [HttpPost]
        public RedirectToActionResult Search(SearchViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var search = new SearchData(TempData)
                {
                    SearchTerm = vm.SearchTerm,
                    Type = vm.Type
                };
                return RedirectToAction("Search");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ViewResult Search()
        {
            var search = new SearchData(TempData);

            if (search.HasSearchTerm)
            {
                var vm = new SearchViewModel
                {
                    SearchTerm = search.SearchTerm
                };

                var options = new QueryOptions<Event>
                {
                    Includes = "Category, EventOrganizers.Organizer"
                };
                if (search.IsEvent)
                {
                    options.Where = b => b.Title.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for event title '{vm.SearchTerm}'";
                }
                if (search.IsOrganizer)
                {
                    int index = vm.SearchTerm.LastIndexOf(' ');
                    if (index == -1)
                    {
                        options.Where = b => b.EventOrganizers.Any(
                            ba => ba.Organizer.OrganizerName.Contains(vm.SearchTerm));
                    }
                    else
                    {
                        string first = vm.SearchTerm.Substring(0, index);

                        options.Where = b => b.EventOrganizers.Any(
                            ba => ba.Organizer.OrganizerName.Contains(first)
                            );
                    }
                    vm.Header = $"Search results for organizer '{vm.SearchTerm}'";
                }
                if (search.IsCategory)
                {
                    options.Where = b => b.CategoryId.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for category ID '{vm.SearchTerm}'";
                }
                vm.Events = data.Events.List(options);
                return View("Index", vm);
            }
            else
            {
				var options = new EventQueryOptions
				{
					Includes = "EventOrganizers.Organizer, Category",
					OrderByDirection = "asc",
					PageNumber = 1,
					PageSize = 5
				};
				var latestEvents = data.Events
			  .List(options)
			  .ToList();

				var SearchViewModel = new SearchViewModel
				{
					Events = latestEvents,
				};
				return View("Index", SearchViewModel);
            }
        }

        [HttpGet]
        public ViewResult Add(int id) => GetEvent(id, "Add");

        [HttpPost]
        public IActionResult Add(EventViewModel vm)
        {
            if (ModelState.IsValid)
            {
                data.LoadNewEventOrganizers(vm.Event, vm.SelectedOrganizers);
                if (vm.ImageFile != null)
                {
                    // Save the uploaded image to a physical path on the server
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(vm.ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        vm.ImageFile.CopyTo(fileStream);
                    }

                    // Update the Event's ImagePath property with the saved file path
                    vm.Event.ImagePath = uniqueFileName;
                }
                data.Events.Insert(vm.Event);


                data.Save();

                TempData["message"] = $"{vm.Event.Title} added to Events.";
                return RedirectToAction("Index");
            }
            else
            {
                Load(vm, "Add");
                return View("Event", vm);
            }
        }

        [HttpGet]
        public ViewResult Edit(int id) => GetEvent(id, "Edit");

        [HttpPost]
        public IActionResult Edit(EventViewModel vm)
        {
            if (ModelState.IsValid)
            {
                data.DeleteCurrentEventOrganizers(vm.Event);
                data.LoadNewEventOrganizers(vm.Event, vm.SelectedOrganizers);
                if (vm.ImageFile != null)
                {
                    // Save the uploaded image to a physical path on the server
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(vm.ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        vm.ImageFile.CopyTo(fileStream);
                    }

                    // Update the Event's ImagePath property with the saved file path
                    vm.Event.ImagePath = uniqueFileName;
                }
                data.Events.Update(vm.Event);
                data.Save();

                TempData["message"] = $"{vm.Event.Title} updated.";
                return RedirectToAction("Search");
            }
            else
            {
                Load(vm, "Edit");
                return View("Event", vm);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id) => GetEvent(id, "Delete");

        [HttpPost]
        public IActionResult Delete(EventViewModel vm)
        {
            data.Events.Delete(vm.Event);
            data.Save();
            TempData["message"] = $"{vm.Event.Title} removed from Events.";
            return RedirectToAction("Search");
        }

        private ViewResult GetEvent(int id, string operation)
        {
            var @event = new EventViewModel();
            Load(@event, operation, id);
            return View("Event", @event);
        }
        private void Load(EventViewModel vm, string op, int? id = null)
        {
            if (Operation.IsAdd(op))
            {
                vm.Event = new Event();
            }
            else
            {
                vm.Event = data.Events.Get(new QueryOptions<Event>
                {
                    Includes = "EventOrganizers.Organizer, Category",
                    Where = b => b.EventId == (id ?? vm.Event.EventId)
                });
            }

            vm.SelectedOrganizers = vm.Event.EventOrganizers?.Select(
                ba => ba.Organizer.OrganizerId).ToArray();
            vm.Organizers = data.Organizers.List(new QueryOptions<Organizer>
            {
                OrderBy = a => a.OrganizerName
            });
            vm.Categories = data.Categories.List(new QueryOptions<Category>
            {
                OrderBy = g => g.Name
            });
        }

    }
}