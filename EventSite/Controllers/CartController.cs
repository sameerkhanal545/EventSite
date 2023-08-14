using Microsoft.AspNetCore.Mvc;
using EventSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using EventSite.Models.DomainModels;
using System.Linq;
using System.Collections.Generic;

namespace EventSite.Controllers
{
	public class CartController : Controller
	{
		private IRepository<Event> data { get; set; }
		private IRepository<CartEntity> cartData { get; set; }
		private IRepository<CartItemEntity> cartItemData { get; set; }

		private readonly IEventSiteUnitOfWork unitOfWork;

		private ICart cart { get; set; }
		private UserManager<User> userManager;
		private readonly IHttpContextAccessor httpContextAccessor;

		public CartController(IRepository<Event> rep, ICart c, UserManager<User> userMngr, IHttpContextAccessor httpContextAccessor, IRepository<CartEntity> cartData, IEventSiteUnitOfWork unitOfWork, IRepository<CartItemEntity> cartItemData)
		{
			data = rep;
			cart = c;
			cart.Load(data);
			userManager = userMngr;
			this.httpContextAccessor = httpContextAccessor;
			this.cartData = cartData;
			this.cartItemData = cartItemData;
			this.unitOfWork = unitOfWork;
		}

		public ViewResult Index()
		{
			var user = userManager.GetUserAsync(httpContextAccessor.HttpContext.User).Result;
			var vm = new CartViewModel();
			if (user == null)
			{
				vm = new CartViewModel
				{
					List = cart.List,
					SubTotal = cart.SubTotal
				};
			}
			else
			{
				var options = new QueryOptions<CartEntity>
				{
					Includes = "CartItems.Event.EventOrganizers",
					Where = cart => cart.UserId == user.Id
				};
				var cart = cartData.Get(options);
				if (cart == null)
				{

					return View("CartEmpty");
				}
				var cartItems = cart.CartItems.ToList();

				var cartItemViewModels = cartItems.Select(cartItemEntity => new CartItem
				{
					Event = new EventDTO
					{
						Title = cartItemEntity.Event.Title,
						EventId = cartItemEntity.EventId,
						Price = cartItemEntity.Event.Price,
						ImagePath = cartItemEntity.Event.ImagePath,
						Organizers = cartItemEntity.Event.EventOrganizers
							.Where(ba => ba.Organizer != null)
							.ToDictionary(ba => ba.OrganizerId, ba => ba.Organizer.OrganizerName)
					},
					Quantity = cartItemEntity.Quantity,
					Id = cartItemEntity.Id,
				}).ToList();

				vm = new CartViewModel
				{
					List = cartItemViewModels,
					SubTotal = CalculateSubTotal(cartItemViewModels)
				};

			}

			return View(vm);
		}

		[HttpPost]
		public RedirectToActionResult Add(int id)
		{
			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");
			}

			var @event = data.Get(new QueryOptions<Event>
			{
				Includes = "EventOrganizers.Organizer, Category",
				Where = b => b.EventId == id
			});
			if (@event == null)
			{
				TempData["message"] = "Unable to add event ticket to cart";
			}
			else
			{

				if (User.Identity.IsAuthenticated)
				{
					var user = userManager.GetUserAsync(User).Result;
					var options = new QueryOptions<CartEntity>
					{
						Includes = "CartItems.Event",
						Where = cart => cart.UserId == user.Id
					};
					var userCart = cartData.Get(options);

					if (userCart != null)
					{
						var cartItemEntity = new CartItemEntity
						{
							EventId = @event.EventId,
							Quantity = 1,
							CartId = userCart.Id
						};

						userCart.CartItems.Add(cartItemEntity);
						cartData.Save();
						TempData["message"] = $"{@event.Title} added to cart";

					}
				}
				else
				{
					var dto = new EventDTO();
					dto.Load(@event);
					CartItem item = new CartItem
					{
						Event = dto,
						Quantity = 1
					};
					cart.Add(item);
					cart.Save();

					TempData["message"] = $"{@event.Title} added to cart";
				}
			}
			return RedirectToAction("List", "Event");
		}

		public IActionResult Edit(int id)
		{
			CartItem item = cart.GetById(id);
			if (item == null)
			{
				TempData["message"] = "Unable to locate cart item";
				return RedirectToAction("List");
			}
			else
			{
				return View(item);
			}
		}
		[HttpPost]
		public RedirectToActionResult Edit(CartItem item)
		{
			if (User.Identity.IsAuthenticated)
			{
				CartItemEntity cartItem = cartItemData.Get(item.Id);
				if (cartItem != null)
				{
					cartItem.Quantity = item.Quantity;
					cartItemData.Update(cartItem);
					cartItemData.Save();

				}

			}
			else
			{
				cart.Edit(item);
				cart.Save();
			}
			TempData["message"] = $"{item.Event.Title} updated";
			return RedirectToAction("Index");
		}

		[HttpPost]
		public RedirectToActionResult Remove(int id)
		{
			var name = "";
			if (User.Identity.IsAuthenticated)
			{
				CartItemEntity cartItem = cartItemData.Get(id);
				if (cartItem != null)
				{
					cartItemData.Delete(cartItem);
					cartItemData.Save();

				}
			}
			else
			{
				CartItem item = cart.GetById(id);
				name = item.Event.Title;
				cart.Remove(item);
				cart.Save();
			}
			TempData["message"] = $"Item removed from cart.";
			return RedirectToAction("Index");
		}

		[HttpPost]
		public RedirectToActionResult Clear()
		{
			if (User.Identity.IsAuthenticated)
			{
				var user = userManager.GetUserAsync(User).Result;

				var options = new QueryOptions<CartEntity>
				{
					Includes = "CartItems.Event",
					Where = cart => cart.UserId == user.Id
				};
				var userCart = cartData.Get(options);
				foreach (var cartItem in userCart.CartItems)
				{
					cartItemData.Delete(cartItem);
				}

				cartItemData.Save();
			}
			else
			{
				cart.Clear();
				cart.Save();
			}
			TempData["message"] = "Cart cleared.";
			return RedirectToAction("Index");
		}

		public ViewResult Checkout() => View();

		private double CalculateSubTotal(IEnumerable<CartItem> cartItems)
		{
			return cartItems.Sum(item => item.Event.Price * item.Quantity);
		}
	}
}