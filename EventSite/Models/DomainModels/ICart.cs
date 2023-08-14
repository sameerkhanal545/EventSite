using System.Collections.Generic;

namespace EventSite.Models
{
    public interface ICart
    {
        double SubTotal { get; }
        int? Count { get; }
        IEnumerable<CartItem> List { get; }

        void Load(IRepository<Event> data);
        CartItem GetById(int id);

        void Add(CartItem item);
        void Edit(CartItem item);
        void Remove(CartItem item);
        void Clear();
        void Save();
    }
}