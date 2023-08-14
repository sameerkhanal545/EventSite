using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSite.Models
{
    internal class SeedOrganizer : IEntityTypeConfiguration<Organizer>
    {
        public void Configure(EntityTypeBuilder<Organizer> entity)
        {
            entity.HasData(
                new Organizer { OrganizerId = 1, OrganizerName = "Michelle" },
                new Organizer { OrganizerId = 2, OrganizerName = "Stephen E." },
                new Organizer { OrganizerId = 3, OrganizerName = "Margaret" },
                new Organizer { OrganizerId = 4, OrganizerName = "Jane" },
                new Organizer { OrganizerId = 5, OrganizerName = "James" },
                new Organizer { OrganizerId = 6, OrganizerName = "Emily" },
                new Organizer { OrganizerId = 7, OrganizerName = "Agatha" }
                
            );
        }
    }

}