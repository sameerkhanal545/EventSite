using System;
using Microsoft.AspNetCore.Mvc;
using EventSite.Models;
using Microsoft.AspNetCore.Identity;

namespace EventSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> userManager;

        private Repository<Event> data { get; set; }
        public HomeController(EventSiteContext ctx , UserManager<User> userManager)
        {
            this.userManager = userManager;
            data = new Repository<Event>(ctx);
        }

        public ViewResult Index()
        {
            var currentUser = userManager.GetUserAsync(User).Result;
			DateTime currentDate = DateTime.Now;
			DateTime oneWeekFromNow = currentDate.AddDays(7);

			var options = new QueryOptions<Event>
			{
				Includes = "EventOrganizers.Organizer, Category",
				Where = entity => entity.EventEndDate > currentDate && entity.EventDate <= oneWeekFromNow
			};
           
			var random = data.List(options);

            return View(random);
        }

    }
}