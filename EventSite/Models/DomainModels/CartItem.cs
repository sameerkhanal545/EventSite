using Newtonsoft.Json;

namespace EventSite.Models
{
    public class CartItem
    {
        internal object eventEntity;

		public int Id { get; set; }

		public EventDTO Event { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public double SubTotal => Event.Price * Quantity;
    }
}
