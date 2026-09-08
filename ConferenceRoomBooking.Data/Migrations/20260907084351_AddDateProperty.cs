using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceRoomBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDateProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Bookings",
                type: "date",
                nullable: true);

            migrationBuilder.Sql(
                "UPDATE [Bookings] SET [Date] = CAST([StartTime] AS date) WHERE [Date] IS NULL;");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "Bookings",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Bookings");
        }
    }
}
