using EventSite.Models.DomainModels;
using System.Linq;

namespace EventSite.Models
{
    public class EventSiteUnitOfWork : IEventSiteUnitOfWork
    {
        private EventSiteContext context { get; set; }
        public EventSiteUnitOfWork(EventSiteContext ctx) => context = ctx;

        private Repository<Event> eventData;

        private Repository<CartEntity> cartData;

        private Repository<CartItemEntity> cartItemData;

        public Repository<Event> Events {
            get {
                if (eventData == null)
					eventData = new Repository<Event>(context);
                return eventData;
            }
        }

        private Repository<Organizer> organizerData;
        public Repository<Organizer> Organizers
		{
            get {
                if (organizerData == null)
					organizerData = new Repository<Organizer>(context);
                return organizerData;
            }
        }

        private Repository<EventOrganizer>eventOrganizerData;
        public Repository<EventOrganizer> EventOrganizers
		{
            get {
                if (eventOrganizerData == null)
                    eventOrganizerData = new Repository<EventOrganizer>(context);
                return eventOrganizerData;
            }
        }

        private Repository<Category> categoryData;
        public Repository<Category> Categories
        {
            get {
                if (categoryData == null)
					categoryData = new Repository<Category>(context);
                return categoryData;
            }
        }

        public Repository<CartEntity> Carts
        {
            get
            {
                if (cartData == null)
                    cartData = new Repository<CartEntity>(context);
                return cartData;
            }
        }

        public Repository<CartItemEntity> CartItems
        {
            get
            {
                if (cartItemData == null)
                    cartItemData = new Repository<CartItemEntity>(context);
                return cartItemData;
            }
        }

        public void DeleteCurrentEventOrganizers(Event evt)

		{
            var eventOrganizers = EventOrganizers.List(new QueryOptions<EventOrganizer> {
                Where = ba => ba.EventId == evt.EventId
            });
            foreach (EventOrganizer ba in eventOrganizers) {
				EventOrganizers.Delete(ba);
            }
        }

        public void LoadNewEventOrganizers(Event evt, int[] authorids)
        {
            foreach (int id in authorids) {
				EventOrganizer ba = new EventOrganizer { Event = evt, OrganizerId = id };
                EventOrganizers.Insert(ba);
            }
        }
        public CartEntity FindByUserId(string userId)
        {
            return cartData.List(new QueryOptions<CartEntity>
            {
                Includes = "CartItems",
                Where = cart => cart.UserId == userId
            }).FirstOrDefault();
        }

        public void Save() => context.SaveChanges();
    }
}