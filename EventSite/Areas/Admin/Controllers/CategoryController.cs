using Microsoft.AspNetCore.Mvc;
using EventSite.Models;

namespace EventSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private Repository<Category> data { get; set; }
        public CategoryController(EventSiteContext ctx) => data = new Repository<Category>(ctx);

        public ViewResult Index()
        {
            var search = new SearchData(TempData);
            search.Clear();

            var categories = data.List(new QueryOptions<Category> {
                OrderBy = g => g.Name
            });
            return View(categories);
        }

        [HttpGet]
        public ViewResult Add() => View("Category", new Category());

        [HttpPost]
        public IActionResult Add(Category category)
        {
            var validate = new Validate(TempData);
            if (!validate.IsCategoryChecked) {
                validate.CheckCategory(category.CategoryId, data);
                if (!validate.IsValid) {
                    ModelState.AddModelError(nameof(category.CategoryId), validate.ErrorMessage);
                }     
            }

            if (ModelState.IsValid) {
                data.Insert(category);
                data.Save();
                validate.ClearCategory();
                TempData["message"] = $"{category.Name} added to Categories.";
                return RedirectToAction("Index");  
            }
            else {
                return View("Category", category);
            }
        }

        [HttpGet]
        public ViewResult Edit(string id) => View("Category", data.Get(id));

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid) {
                data.Update(category);
                data.Save();
                TempData["message"] = $"{category.Name} updated.";
                return RedirectToAction("Index");  
            }
            else {
                return View("Category", category);
            }
        }

        [HttpGet]
        public IActionResult Delete(string id) {
            var category = data.Get(new QueryOptions<Category> {
                Includes = "Events",
                Where = g => g.CategoryId == id
            });

            if (category.Events.Count > 0) {
                TempData["message"] = $"Can't delete category {category.Name} " 
                                    + "because it's associated with these events.";
                return GoToEventSearchResults(id);
            }
            else {
                return View("Category", category);
            }
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            data.Delete(category);
            data.Save();
            TempData["message"] = $"{category.Name} removed from Categories.";
            return RedirectToAction("Index");  // PRG pattern
        }

        public RedirectToActionResult ViewEvents(string id) {
            RedirectToActionResult result = GoToEventSearchResults(id);
            return result;
        }

        private RedirectToActionResult GoToEventSearchResults(string id)
        {
            var search = new SearchData(TempData) {
                SearchTerm = id,
                Type = "category"
            };
            return RedirectToAction("Search", "Event");
        }

    }
}