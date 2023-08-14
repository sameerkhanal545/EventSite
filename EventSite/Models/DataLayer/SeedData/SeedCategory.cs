using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSite.Models
{
    internal class SeedCategory : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.HasData(
                new Category { CategoryId = "music", Name = "Music and Concerts" },
                new Category { CategoryId = "arts", Name = "Art and Culture" },
                new Category { CategoryId = "foods", Name = "Food and Culinary" },
                new Category { CategoryId = "sports", Name = "Sports and Fitnessn" },
                new Category { CategoryId = "fashion", Name = "Fashion" }
            );
        }
    }

}
