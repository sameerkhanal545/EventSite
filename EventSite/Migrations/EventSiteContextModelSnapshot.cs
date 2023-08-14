﻿// <auto-generated />
using System;
using EventSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventSite.Migrations
{
    [DbContext(typeof(EventSiteContext))]
    partial class EventSiteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventSite.Models.Category", b =>
                {
                    b.Property<string>("CategoryId")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = "music",
                            Name = "Music and Concerts"
                        },
                        new
                        {
                            CategoryId = "arts",
                            Name = "Art and Culture"
                        },
                        new
                        {
                            CategoryId = "foods",
                            Name = "Food and Culinary"
                        },
                        new
                        {
                            CategoryId = "sports",
                            Name = "Sports and Fitnessn"
                        },
                        new
                        {
                            CategoryId = "fashion",
                            Name = "Fashion"
                        });
                });

            modelBuilder.Entity("EventSite.Models.DomainModels.CartEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("EventSite.Models.DomainModels.CartItemEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CartEntityId")
                        .HasColumnType("int");

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartEntityId");

                    b.HasIndex("EventId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("EventSite.Models.DomainModels.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("OrderTotal")
                        .HasColumnType("float");

                    b.Property<int?>("ShippingAddressId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderId");

                    b.HasIndex("ShippingAddressId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EventSite.Models.DomainModels.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailId"));

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("float");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("EventId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("EventSite.Models.DomainModels.ShippingAddress", b =>
                {
                    b.Property<int>("ShippingAddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShippingAddressId"));

                    b.Property<string>("AddressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ShippingAddressId");

                    b.ToTable("ShippingAddress");
                });

            modelBuilder.Entity("EventSite.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventId"));

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EventEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("EventId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            EventId = 1,
                            CategoryId = "music",
                            Description = "Music Event",
                            EventDate = new DateTime(2023, 8, 15, 18, 30, 0, 0, DateTimeKind.Unspecified),
                            EventEndDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImagePath = "",
                            Location = "Main Concert Hall",
                            Price = 18.0,
                            Title = "1776"
                        },
                        new
                        {
                            EventId = 2,
                            CategoryId = "arts",
                            Description = "Art Exhibition",
                            EventDate = new DateTime(2023, 8, 20, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            EventEndDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImagePath = "",
                            Location = "Gallery 3",
                            Price = 5.5,
                            Title = "1984 Paintings"
                        },
                        new
                        {
                            EventId = 3,
                            CategoryId = "foods",
                            Description = "Culinary Adventure",
                            EventDate = new DateTime(2023, 8, 25, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            EventEndDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImagePath = "",
                            Location = "Downtown Square",
                            Price = 4.5,
                            Title = "Asian Street Food Festival"
                        },
                        new
                        {
                            EventId = 4,
                            CategoryId = "sports",
                            Description = "Cricket Match",
                            EventDate = new DateTime(2023, 8, 28, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            EventEndDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImagePath = "",
                            Location = "City Cricket Stadium",
                            Price = 11.5,
                            Title = "T20 International"
                        },
                        new
                        {
                            EventId = 5,
                            CategoryId = "fashion",
                            Description = "Runway Extravaganza",
                            EventDate = new DateTime(2023, 9, 5, 20, 0, 0, 0, DateTimeKind.Unspecified),
                            EventEndDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImagePath = "",
                            Location = "Grand Ballroom",
                            Price = 10.99,
                            Title = "Fashion Show"
                        });
                });

            modelBuilder.Entity("EventSite.Models.EventOrganizer", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("OrganizerId")
                        .HasColumnType("int");

                    b.HasKey("EventId", "OrganizerId");

                    b.HasIndex("OrganizerId");

                    b.ToTable("EventOrganizers");

                    b.HasData(
                        new
                        {
                            EventId = 1,
                            OrganizerId = 1
                        },
                        new
                        {
                            EventId = 2,
                            OrganizerId = 2
                        },
                        new
                        {
                            EventId = 3,
                            OrganizerId = 5
                        },
                        new
                        {
                            EventId = 4,
                            OrganizerId = 7
                        },
                        new
                        {
                            EventId = 5,
                            OrganizerId = 6
                        });
                });

            modelBuilder.Entity("EventSite.Models.Organizer", b =>
                {
                    b.Property<int>("OrganizerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrganizerId"));

                    b.Property<string>("OrganizerName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("OrganizerId");

                    b.ToTable("Organizers");

                    b.HasData(
                        new
                        {
                            OrganizerId = 1,
                            OrganizerName = "Michelle"
                        },
                        new
                        {
                            OrganizerId = 2,
                            OrganizerName = "Stephen E."
                        },
                        new
                        {
                            OrganizerId = 3,
                            OrganizerName = "Margaret"
                        },
                        new
                        {
                            OrganizerId = 4,
                            OrganizerName = "Jane"
                        },
                        new
                        {
                            OrganizerId = 5,
                            OrganizerName = "James"
                        },
                        new
                        {
                            OrganizerId = 6,
                            OrganizerName = "Emily"
                        },
                        new
                        {
                            OrganizerId = 7,
                            OrganizerName = "Agatha"
                        });
                });

            modelBuilder.Entity("EventSite.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("EventSite.Models.DomainModels.CartEntity", b =>
                {
                    b.HasOne("EventSite.Models.User", "User")
                        .WithOne("Cart")
                        .HasForeignKey("EventSite.Models.DomainModels.CartEntity", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventSite.Models.DomainModels.CartItemEntity", b =>
                {
                    b.HasOne("EventSite.Models.DomainModels.CartEntity", null)
                        .WithMany("CartItems")
                        .HasForeignKey("CartEntityId");

                    b.HasOne("EventSite.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("EventSite.Models.DomainModels.Order", b =>
                {
                    b.HasOne("EventSite.Models.DomainModels.ShippingAddress", "ShippingAddress")
                        .WithMany()
                        .HasForeignKey("ShippingAddressId");

                    b.Navigation("ShippingAddress");
                });

            modelBuilder.Entity("EventSite.Models.DomainModels.OrderDetail", b =>
                {
                    b.HasOne("EventSite.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventSite.Models.DomainModels.Order", null)
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("EventSite.Models.Event", b =>
                {
                    b.HasOne("EventSite.Models.Category", "Category")
                        .WithMany("Events")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("EventSite.Models.EventOrganizer", b =>
                {
                    b.HasOne("EventSite.Models.Event", "Event")
                        .WithMany("EventOrganizers")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventSite.Models.Organizer", "Organizer")
                        .WithMany("EventOrganizers")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("EventSite.Models.Category", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("EventSite.Models.DomainModels.CartEntity", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("EventSite.Models.DomainModels.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("EventSite.Models.Event", b =>
                {
                    b.Navigation("EventOrganizers");
                });

            modelBuilder.Entity("EventSite.Models.Organizer", b =>
                {
                    b.Navigation("EventOrganizers");
                });

            modelBuilder.Entity("EventSite.Models.User", b =>
                {
                    b.Navigation("Cart");
                });
#pragma warning restore 612, 618
        }
    }
}
