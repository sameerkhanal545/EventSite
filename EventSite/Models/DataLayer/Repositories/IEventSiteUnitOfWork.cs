using EventSite.Models.DomainModels;

namespace EventSite.Models
{
    public interface IEventSiteUnitOfWork
    {
        Repository<Event> Events { get; }
        Repository<Organizer> Organizers { get; }
        Repository<EventOrganizer> EventOrganizers { get; }
        Repository<Category> Categories { get; }
        Repository<CartEntity> Carts { get; }
        Repository<CartItemEntity> CartItems { get; }

        void DeleteCurrentEventOrganizers(Event evt);
        void LoadNewEventOrganizers(Event cvt, int[] authorids);
        void Save();
    }
}
