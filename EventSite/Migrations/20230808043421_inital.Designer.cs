﻿// <auto-generated />
using System;
using EventSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventSite.Migrations
{
    [DbContext(typeof(EventSiteContext))]
    [Migration("20230808043421_inital")]
    partial class inital
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
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

            modelBuilder.Entity("EventSite.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventId"));

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

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
                            Price = 18.0,
                            Title = "1776"
                        },
                        new
                        {
                            EventId = 2,
                            CategoryId = "arts",
                            Price = 5.5,
                            Title = "1984 Paintings"
                        },
                        new
                        {
                            EventId = 3,
                            CategoryId = "foods",
                            Price = 4.5,
                            Title = "Asian Street food Festival"
                        },
                        new
                        {
                            EventId = 4,
                            CategoryId = "sports",
                            Price = 11.5,
                            Title = "T20I"
                        },
                        new
                        {
                            EventId = 5,
                            CategoryId = "fashion",
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

            modelBuilder.Entity("EventSite.Models.Event", b =>
                {
                    b.Navigation("EventOrganizers");
                });

            modelBuilder.Entity("EventSite.Models.Organizer", b =>
                {
                    b.Navigation("EventOrganizers");
                });
#pragma warning restore 612, 618
        }
    }
}
