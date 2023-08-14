using Microsoft.AspNetCore.Mvc;
using EventSite.Models;

namespace EventSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrganizerController : Controller
    {
        private Repository<Organizer> data { get; set; }
        public OrganizerController(EventSiteContext ctx) => data = new Repository<Organizer>(ctx);

        public ViewResult Index()
        {
            var authors = data.List(new QueryOptions<Organizer> {
                OrderBy = a => a.OrganizerName
            });
            return View(authors);
        }

        public RedirectToActionResult Select(int id, string operation)
        {
            switch (operation.ToLower())
            {
                case "view events":
                    return RedirectToAction("ViewEvents", new { id });
                case "edit":
                    return RedirectToAction("Edit", new { id });
                case "delete":
                    return RedirectToAction("Delete", new { id });
                default:
                    return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ViewResult Add() => View("Organizer", new Organizer());

        [HttpPost]
        public IActionResult Add(Organizer organizer, string operation)
        {
            var validate = new Validate(TempData);
            if (!validate.IsOrganizerChecked) {
                validate.CheckOrganizer(organizer.OrganizerName, operation, data);
                if (!validate.IsValid) {
                    ModelState.AddModelError(nameof(organizer.OrganizerName), validate.ErrorMessage);
                }    
            }
            
            if (ModelState.IsValid) {
                data.Insert(organizer);
                data.Save();
                validate.ClearOrganizer();
                TempData["message"] = $"{organizer.OrganizerName} added to Oranizers name.";
                return RedirectToAction("Index");  
            }
            else {
                return View("Organizer", organizer);
            }
        }

        [HttpGet]
        public ViewResult Edit(int id) => View("Organizer", data.Get(id));

        [HttpPost]
        public IActionResult Edit(Organizer organizer)
        {
            if (ModelState.IsValid) {
                data.Update(organizer);
                data.Save();
                TempData["message"] = $"{organizer.OrganizerName} updated.";
                return RedirectToAction("Index");  
            }
            else {
                return View("Organizer", organizer);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var organizer = data.Get(new QueryOptions<Organizer> {
                Includes = "EventOrganizers",
                Where = a => a.OrganizerId == id
            });

            if (organizer.EventOrganizers.Count > 0) {
                TempData["message"] = $"Can't delete organizer {organizer.OrganizerName} because they are associated with these events.";
                return GoToOrganizerSearch(organizer);
            }
            else {
                return View("Organizer", organizer);
            }
        }

        [HttpPost]
        public RedirectToActionResult Delete(Organizer author)
        {
            data.Delete(author);
            data.Save();
            TempData["message"] = $"{author.OrganizerName} removed from Organizers.";
            return RedirectToAction("Index");  
        }

        public RedirectToActionResult ViewEvents(int id)
        {
            var organizer = data.Get(id);
            return GoToOrganizerSearch(organizer);
        }

        private RedirectToActionResult GoToOrganizerSearch(Organizer organizer)
        {
            var search = new SearchData(TempData) {
                SearchTerm = organizer.OrganizerName,
                Type = "organizer"
            };
            return RedirectToAction("Search", "Event");
        }
    }
}