using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportCompanies.Migrations
{
    /// <inheritdoc />
    public partial class UsingViaCepToGetAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Destination_Bairro",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Destination_Cidade",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Destination_Estado",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Destination_Rua",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Origin_Bairro",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Origin_Cidade",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Origin_Estado",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Origin_Rua",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination_Bairro",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Destination_Cidade",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Destination_Estado",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Destination_Rua",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Origin_Bairro",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Origin_Cidade",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Origin_Estado",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Origin_Rua",
                table: "Orders");
        }
    }
}
