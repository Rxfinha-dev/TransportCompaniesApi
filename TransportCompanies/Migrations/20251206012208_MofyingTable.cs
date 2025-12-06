using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportCompanies.Migrations
{
    /// <inheritdoc />
    public partial class MofyingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDispatched",
                table: "Statuses");

            migrationBuilder.AddColumn<bool>(
                name: "IsDispatched",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDispatched",
                table: "Orders");

            migrationBuilder.AddColumn<bool>(
                name: "IsDispatched",
                table: "Statuses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
