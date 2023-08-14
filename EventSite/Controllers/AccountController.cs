using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EventSite.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using EventSite.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace EventSite.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private readonly IEventSiteUnitOfWork data;
        private ICart cart;

        public AccountController(UserManager<User> userMngr,
            SignInManager<User> signInMngr, EventSiteContext ctx, ICart cart)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            data = new EventSiteUnitOfWork(ctx);
            this.cart = cart;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
            };

                    await userManager.AddClaimsAsync(user, claims);

                    var cart = new CartEntity { UserId = user.Id };
                    data.Carts.Insert(cart);

                    data.Save();

                    bool isPersistent = false;
                    await signInManager.SignInAsync(user, isPersistent);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors) 
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult LogIn(string returnURL = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnURL };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {                
                var result = await signInManager.PasswordSignInAsync(
                    model.Username, model.Password, isPersistent: model.RememberMe, 
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var currentUser = await userManager.FindByNameAsync(model.Username);

                    if (currentUser != null)
                    {
                        Console.WriteLine("User authenticated successfully");
						var options = new QueryOptions<CartEntity>
						{
							Includes = "CartItems.Event",
							Where = cart => cart.UserId == currentUser.Id
						};
						var userCart = data.Carts.Get(options);
                        if(userCart == null)
                        {
							var cart = new CartEntity { UserId = currentUser.Id };
							data.Carts.Insert(cart);

							data.Save();

						}
						if (!string.IsNullOrEmpty(model.ReturnUrl) &&
                            Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            ModelState.AddModelError("", "Invalid username/password.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public ViewResult AccessDenied()
        {
            return View();
        }
    }
}