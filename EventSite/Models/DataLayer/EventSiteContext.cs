using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using EventSite.Models.DomainModels;

namespace EventSite.Models
{
    public class EventSiteContext : IdentityDbContext<User>
    {
        public EventSiteContext(DbContextOptions<EventSiteContext> options)
            : base(options)
        { }

        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventOrganizer> EventOrganizers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartEntity> Carts { get; set; }

        public DbSet<CartItemEntity> CartItems { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<ShippingAddress> ShippingAddress { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventOrganizer>().HasKey(ba => new { ba.EventId, ba.OrganizerId });

            modelBuilder.Entity<EventOrganizer>().HasOne(ba => ba.Event)
                .WithMany(b => b.EventOrganizers)
                .HasForeignKey(ba => ba.EventId);
            modelBuilder.Entity<EventOrganizer>().HasOne(ba => ba.Organizer)
                .WithMany(a => a.EventOrganizers)
                .HasForeignKey(ba => ba.OrganizerId);

            modelBuilder.Entity<Event>().HasOne(b => b.Category)
                .WithMany(g => g.Events)
                .OnDelete(DeleteBehavior.Restrict);

            // seed initial data for category event and organizer
            modelBuilder.ApplyConfiguration(new SeedCategory());
            modelBuilder.ApplyConfiguration(new SeedEvents());
            modelBuilder.ApplyConfiguration(new SeedOrganizer());
            modelBuilder.ApplyConfiguration(new SeedEventOrganizers());
            modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();

            modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();

            modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();

        }
        public static async Task CreateAdminUser(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Check if an admin user already exists
            var adminUser = await userManager.FindByNameAsync("admin");
            Console.WriteLine(adminUser);
            if (adminUser == null)
            {
                // Create a new admin user
                adminUser = new User
                {
                    UserName = "admin",
                    Email = "admin@example.com"
                };

                // Create the admin user with a password
                var createResult = await userManager.CreateAsync(adminUser, "Admin@123");

                if (createResult.Succeeded)
                {
                    // Check if the "Administrator" role exists
                    var adminRoleExists = await roleManager.RoleExistsAsync("Admin");

                    if (!adminRoleExists)
                    {
                        // Create the "Admin" role if it doesn't exist
                        await roleManager.CreateAsync(new IdentityRole("Admin"));
                    }


                    if (createResult.Succeeded)
                    {
                        // Add the user to the "Admin" role
                        var addToRoleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }
            }
        }

    }
}
