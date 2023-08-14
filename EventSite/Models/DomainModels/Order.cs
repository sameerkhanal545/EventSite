using System.Collections.Generic;
using System;

namespace EventSite.Models.DomainModels
{
	public class Order
	{
		public int OrderId { get; set; }
		public DateTime OrderDate { get; set; }
		public string UserId { get; set; }
        public double OrderTotal { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
		public ShippingAddress ShippingAddress { get; set; }
	}
}
