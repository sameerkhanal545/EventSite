using System.Collections.Generic;

namespace EventSite.Models.DomainModels
{
    public class CartEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; } 
        public List<CartItemEntity> CartItems { get; set; } = new List<CartItemEntity>(); 
    }
}
