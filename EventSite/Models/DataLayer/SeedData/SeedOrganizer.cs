using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSite.Models
{
    internal class SeedOrganizer : IEntityTypeConfiguration<Organizer>
    {
        public void Configure(EntityTypeBuilder<Organizer> entity)
        {
            entity.HasData(
                new Organizer { OrganizerId = 1, OrganizerName = "KW Inc" },
                new Organizer { OrganizerId = 2, OrganizerName = "Peters Events" },
                new Organizer { OrganizerId = 3, OrganizerName = "Jane Management" },
                new Organizer { OrganizerId = 4, OrganizerName = "Quic Events Inc" },
                new Organizer { OrganizerId = 5, OrganizerName = "Total Inc" },
                new Organizer { OrganizerId = 6, OrganizerName = "Samy Inc" },
                new Organizer { OrganizerId = 7, OrganizerName = "Petty Inc" }
                
            );
        }
    }

}