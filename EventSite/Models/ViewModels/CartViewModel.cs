using System.Collections.Generic;

namespace EventSite.Models
{
    public class CartViewModel 
    {
        public IEnumerable<CartItem> List { get; set; }
        public double SubTotal { get; set; }
        public RouteDictionary EventGridRoute { get; set; }
    }
}
