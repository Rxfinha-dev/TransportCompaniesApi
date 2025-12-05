using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportCompanies.Migrations
{
    /// <inheritdoc />
    public partial class CreatingIsDispatched : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDispatched",
                table: "Statuses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDispatched",
                table: "Statuses");
        }
    }
}
