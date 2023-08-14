using EventSite.Models.DomainModels;
using EventSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System;
using System.Linq;

namespace EventSite.Controllers
{
    public class OrderController : Controller
    {
		private IRepository<Event> data { get; set; }
		private IRepository<CartEntity> cartData { get; set; }

		private IRepository<CartItemEntity> cartItemData { get; set; }

        private IRepository<Order> orderData { get; set; }


        private UserManager<User> userManager;
		private readonly IHttpContextAccessor httpContextAccessor;

		public OrderController(
		IRepository<Event> eventData,
		IRepository<CartEntity> cartData,
		IRepository<CartItemEntity> cartItemData,
		UserManager<User> userManager,
		IHttpContextAccessor httpContextAccessor,
        IRepository<Order> orderData)
		{
			this.data = eventData;
			this.cartData = cartData;
			this.cartItemData = cartItemData;
			this.userManager = userManager;
			this.httpContextAccessor = httpContextAccessor;
			this.orderData = orderData;
		}

		public IActionResult Index()
        {
            return View();
        }
		public IActionResult Checkout()
		{

			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account"); 
			}

            var checkoutViewModel = GetUsersOrderDetails();
		

			return View(checkoutViewModel);
		
		}

    

        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {

                var user = userManager.GetUserAsync(User).Result;

                var options = new QueryOptions<CartEntity>
                {
                    Includes = "CartItems.Event",
                    Where = cart => cart.UserId == user.Id
                };
                var cart = cartData.Get(options);

                var orderDetails = ConvertCartItemsToOrderDetailsView(cart.CartItems);

                double orderTotal = 0;

                foreach (var orderDetail in orderDetails)
                {
                    orderTotal += orderDetail.UnitPrice * orderDetail.Quantity;
                }

                // Creating the order entity
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    OrderTotal = orderTotal,
                    UserId = user.Id,
                    ShippingAddress = new ShippingAddress
                    {
                        AddressLine1 = model.AddressLine1,
                        AddressLine2 = model.AddressLine2,
                        FullName= model.FullName,
                        City= model.City,
                        State= model.State,
                        PostalCode= model.PostalCode,
                        UserId = user.Id,
                    }
                };

                foreach (var orderDetail in orderDetails)
                {
                    order.OrderDetails.Add(orderDetail);
                }
                orderData.Insert(order);
                orderData.Save();

                ClearCartItems(userManager.GetUserId(User));

                TempData["message"] = $"Order Succefully Created";
                return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
            }

            var checkoutViewModel = GetUsersOrderDetails();
            model.OrderDetails = checkoutViewModel.OrderDetails;
            model.OrderTotal = checkoutViewModel.OrderTotal;
            return View("Checkout", model);
        }

		public IActionResult Orders()
		{

			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");
			}
			var user = userManager.GetUserAsync(User).Result;


			var options = new QueryOptions<Order>
			{
				Includes = "OrderDetails.Event , ShippingAddress",
				Where = order => order.UserId == user.Id
			};
			var fetchedOrder = orderData.List(options);
            var orderModel = ConvertOrderToOrderViewModel(fetchedOrder);

			return View(orderModel);

		}

		private List<OrderViewModel> ConvertOrderToOrderViewModel(IEnumerable<Order> fetchedOrder)
		{
            List< OrderViewModel> orderViewModels = new List<OrderViewModel>();
			foreach (var orderItem in fetchedOrder)
			{
			

				var orderDetail = new OrderViewModel
				{
					OrderDate = orderItem.OrderDate,
					 OrderId= orderItem.OrderId,
					OrderTotal = orderItem.OrderTotal,
					FullName = orderItem.ShippingAddress.FullName,
                    AddressLine1 = orderItem.ShippingAddress.AddressLine1,
                    City= orderItem.ShippingAddress.City,
                    State= orderItem.ShippingAddress.State,
				};

				orderViewModels.Add(orderDetail);
			}
            return orderViewModels;
		}

		public IActionResult OrderDetails(int id)
        {


            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var options = new QueryOptions<Order>
            {
                Includes = "OrderDetails.Event",
                Where = order => order.OrderId == id
            };
            var fetchedOrder = orderData.Get(options);


            var orderDetailsViewModel = new OrderDetailsViewModel
            {
                OrderId = fetchedOrder.OrderId,
                OrderDate = fetchedOrder.OrderDate,
                OrderTotal = fetchedOrder.OrderTotal,
                OrderDetails = ConvertOrderToOrderDetailViewModel(fetchedOrder.OrderDetails)
            };

            return View(orderDetailsViewModel);
        }

        private List<OrderDetailViewModel> ConvertOrderToOrderDetailViewModel(List<OrderDetail> orderDetails)
        {
            var orderDetailmodels = new List<OrderDetailViewModel>();

            foreach (var orderItem in orderDetails)
            {
                var eventDto = new EventDTO
                {
                    EventId = orderItem.Event.EventId,
                    Title = orderItem.Event.Title,
                    Price = orderItem.Event.Price,

                };

                var orderDetail = new OrderDetailViewModel
                {
                    Event = eventDto,
                    Quantity = orderItem.Quantity,
                    UnitPrice = eventDto.Price,
                    TotalPrice = orderItem.Quantity * eventDto.Price
                };

                orderDetailmodels.Add(orderDetail);
            }
            return orderDetailmodels;
        }

        private CheckoutViewModel GetUsersOrderDetails()
        {
            var user = userManager.GetUserAsync(User).Result;

            var options = new QueryOptions<CartEntity>
            {
                Includes = "CartItems.Event",
                Where = cart => cart.UserId == user.Id
            };
            var cart = cartData.Get(options);

            var orderDetails = ConvertCartItemsToOrderDetailsViewModel(cart.CartItems);

            var checkoutViewModel = new CheckoutViewModel
            {
                OrderDetails = orderDetails,
                OrderTotal = CalculateOrderTotal(orderDetails)
            };
            return checkoutViewModel;
        }
        private void ClearCartItems(string id)
        {
            var options = new QueryOptions<CartEntity>
            {
                Includes = "CartItems",
                Where = cart => cart.UserId == id
            };
            var userCart = cartData.Get(options);
            foreach (var cartItem in userCart.CartItems)
            {
                cartItemData.Delete(cartItem);
            }

            cartItemData.Save();
        }

        private List<OrderDetailViewModel> ConvertCartItemsToOrderDetailsViewModel(IEnumerable<CartItemEntity> cartItems)
		{
			var orderDetails = new List<OrderDetailViewModel>();

			foreach (var cartItem in cartItems)
			{
				var eventDto = new EventDTO
				{
					EventId = cartItem.Event.EventId,
					Title = cartItem.Event.Title,
					Price = cartItem.Event.Price,
					
				};

				var orderDetail = new OrderDetailViewModel
				{
					Event = eventDto,
					Quantity = cartItem.Quantity,
					UnitPrice = eventDto.Price, 
					TotalPrice = cartItem.Quantity * eventDto.Price
				};

				orderDetails.Add(orderDetail);
			}

			return orderDetails;
		}


        private List<OrderDetail> ConvertCartItemsToOrderDetailsView(IEnumerable<CartItemEntity> cartItems)
        {
            var orderDetails = new List<OrderDetail>();

            foreach (var cartItem in cartItems)
            {

                var orderDetail = new OrderDetail
                {
                    Event = cartItem.Event,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Event.Price,
                    EventId= cartItem.Event.EventId,
                };

                orderDetails.Add(orderDetail);
            }

            return orderDetails;
        }


        private double CalculateOrderTotal(List<OrderDetailViewModel> orderDetails)
		{
			double orderTotal = 0;

			foreach (var orderDetail in orderDetails)
			{
				orderTotal += orderDetail.TotalPrice;
			}

			return orderTotal;
		}


	}
}
