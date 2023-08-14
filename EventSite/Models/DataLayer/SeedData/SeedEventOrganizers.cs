using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSite.Models
{
    internal class SeedEventOrganizers : IEntityTypeConfiguration<EventOrganizer>
    {
        public void Configure(EntityTypeBuilder<EventOrganizer> entity)
        {
            entity.HasData(
                new EventOrganizer { EventId = 1, OrganizerId = 1 },
                new EventOrganizer { EventId = 2, OrganizerId = 2 },
                new EventOrganizer { EventId = 3, OrganizerId = 5 },
                new EventOrganizer { EventId = 4, OrganizerId = 7 },
                new EventOrganizer { EventId = 5, OrganizerId = 6 }
               
            );
        }
    }

}
