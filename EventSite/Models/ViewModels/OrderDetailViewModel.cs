namespace EventSite.Models
{
    public class OrderDetailViewModel
    {
        public EventDTO Event { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
		public double TotalPrice { get; set; }

	}
}
