using System.Linq;
using System.Collections.Generic;

namespace EventSite.Models
{
    public static class CartItemListExtensions
    {
        public static List<CartItemDTO> ToDTO(this List<CartItem> list) =>
            list.Select(ci => new CartItemDTO {
                EventId = ci.Event.EventId,
                Quantity = ci.Quantity
            }).ToList();
    }
}
