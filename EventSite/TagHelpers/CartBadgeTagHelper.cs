using Microsoft.AspNetCore.Razor.TagHelpers;
using EventSite.Models;
using EventSite.Models.DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EventSite.TagHelpers
{
	[HtmlTargetElement("my-cart-badge")]
	public class CartBadgeTagHelper : TagHelper
	{
		private IRepository<CartEntity> cartData { get; set; }
		private UserManager<User> userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CartBadgeTagHelper(IRepository<CartEntity> cartData, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
		{
			this.cartData = cartData;
			this.userManager = userManager;
			this._httpContextAccessor = httpContextAccessor;

		}

		public bool MyCartBadge { get; set; }

		public override void Process(TagHelperContext context,
		TagHelperOutput output)
		{
			var currentUser = _httpContextAccessor.HttpContext.User;

			if (currentUser.Identity.IsAuthenticated) { 

				var options = new QueryOptions<CartEntity>
				{
					Includes = "CartItems",
					Where = cart => cart.UserId == currentUser.FindFirstValue(ClaimTypes.NameIdentifier)
				};
				var cart = cartData.Get(options);
				if (cart != null)
					output.Content.SetContent(cart.CartItems.Count.ToString());
				else
				output.Content.SetContent("0");
			}
			else
			{
				output.Content.SetContent("0");

			}
		}
	}
}
