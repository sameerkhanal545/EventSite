using Microsoft.AspNetCore.Mvc;
using EventSite.Models;

namespace EventSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ValidationController : Controller
    {
        public JsonResult CheckCategory(string categoryId, [FromServices]IRepository<Category> data)
        {
            var validate = new Validate(TempData);
            validate.CheckCategory(categoryId, data);
            if (validate.IsValid) {
                validate.MarkCategoryChecked();
                return Json(true);
            }
            else {
                return Json(validate.ErrorMessage);
            }
        }

        public JsonResult CheckOrganizer(string organizerName, string operation, [FromServices]IRepository<Organizer> data)
        {
            var validate = new Validate(TempData);
            validate.CheckOrganizer(organizerName, operation, data);
            if (validate.IsValid) {
                validate.MarkOrganizerChecked();
                return Json(true);
            }
            else {
                return Json(validate.ErrorMessage);
            }
        }

    }
}