using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EventSite.Models
{
    internal class SeedEvents : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> entity)
        {
            entity.HasData(
               new Event
               {
                   EventId = 1,
                   Title = "1776",
                   CategoryId = "music",
                   Price = 18.00,
                   ImagePath = "",
                   Description = "Music Event",
                   Location = "Main Concert Hall",
                   EventDate = new DateTime(2023, 08, 15, 18, 30, 0),
                   EventEndDate = new DateTime(2023, 08, 15, 18, 30, 0)
               },
new Event
{
    EventId = 2,
    Title = "1984 Paintings",
    CategoryId = "arts",
    Price = 5.50,
    ImagePath = "",
    Description = "Art Exhibition",
    Location = "Gallery 3",
    EventDate = new DateTime(2023, 08, 20, 10, 0, 0),
	EventEndDate = new DateTime(2023, 08, 15, 18, 30, 0)

},
new Event
{
    EventId = 3,
    Title = "Asian Street Food Festival",
    CategoryId = "foods",
    Price = 4.50,
    ImagePath = "",
    Description = "Culinary Adventure",
    Location = "Downtown Square",
    EventDate = new DateTime(2023, 08, 25, 16, 0, 0),
	EventEndDate = new DateTime(2023, 08, 15, 18, 30, 0)

},
new Event
{
    EventId = 4,
    Title = "T20 International",
    CategoryId = "sports",
    Price = 11.50,
    ImagePath = "",
    Description = "Cricket Match",
    Location = "City Cricket Stadium",
    EventDate = new DateTime(2023, 08, 28, 14, 0, 0),
	EventEndDate = new DateTime(2023, 08, 15, 18, 30, 0)

},
new Event
{
    EventId = 5,
    Title = "Fashion Show",
    CategoryId = "fashion",
    Price = 10.99,
    ImagePath = "",
    Description = "Runway Extravaganza",
    Location = "Grand Ballroom",
    EventDate = new DateTime(2023, 09, 05, 20, 0, 0),
	EventEndDate = new DateTime(2023, 08, 15, 18, 30, 0)

}

			);
        }
    }

}
