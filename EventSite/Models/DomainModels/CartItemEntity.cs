namespace EventSite.Models.DomainModels
{
    public class CartItemEntity
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; } 
        public int Quantity { get; set; }
        public int CartId { get; set; }
    }
}
