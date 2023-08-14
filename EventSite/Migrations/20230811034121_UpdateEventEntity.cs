using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventSite.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EventDate",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "Description", "EventDate", "Location" },
                values: new object[] { "Music Event", new DateTime(2023, 8, 15, 18, 30, 0, 0, DateTimeKind.Unspecified), "Main Concert Hall" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 2,
                columns: new[] { "Description", "EventDate", "Location" },
                values: new object[] { "Art Exhibition", new DateTime(2023, 8, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), "Gallery 3" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 3,
                columns: new[] { "Description", "EventDate", "Location", "Title" },
                values: new object[] { "Culinary Adventure", new DateTime(2023, 8, 25, 16, 0, 0, 0, DateTimeKind.Unspecified), "Downtown Square", "Asian Street Food Festival" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 4,
                columns: new[] { "Description", "EventDate", "Location", "Title" },
                values: new object[] { "Cricket Match", new DateTime(2023, 8, 28, 14, 0, 0, 0, DateTimeKind.Unspecified), "City Cricket Stadium", "T20 International" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 5,
                columns: new[] { "Description", "EventDate", "Location" },
                values: new object[] { "Runway Extravaganza", new DateTime(2023, 9, 5, 20, 0, 0, 0, DateTimeKind.Unspecified), "Grand Ballroom" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 3,
                column: "Title",
                value: "Asian Street food Festival");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 4,
                column: "Title",
                value: "T20I");
        }
    }
}
