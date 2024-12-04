using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreReactApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFlight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stops",
                table: "Flights",
                newName: "StartNumberOfStops");

            migrationBuilder.RenameColumn(
                name: "Departure",
                table: "Flights",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Arrival",
                table: "Flights",
                newName: "StartArrivalDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndArrivalDate",
                table: "Flights",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Flights",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EndNumberOfStops",
                table: "Flights",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOneWay",
                table: "Flights",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndArrivalDate",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "EndNumberOfStops",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "IsOneWay",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "StartNumberOfStops",
                table: "Flights",
                newName: "Stops");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Flights",
                newName: "Departure");

            migrationBuilder.RenameColumn(
                name: "StartArrivalDate",
                table: "Flights",
                newName: "Arrival");
        }
    }
}
