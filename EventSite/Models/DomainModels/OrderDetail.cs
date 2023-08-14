namespace EventSite.Models.DomainModels
{
	public class OrderDetail
	{
		public int OrderDetailId { get; set; }
		public int OrderId { get; set; }
		public int EventId { get; set; }
		public int Quantity { get; set; }
		public double UnitPrice { get; set; }
		public Event Event { get; set; }


	}
}
