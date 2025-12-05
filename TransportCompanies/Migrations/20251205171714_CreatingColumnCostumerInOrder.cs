using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportCompanies.Migrations
{
    /// <inheritdoc />
    public partial class CreatingColumnCostumerInOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CostumerId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CostumerId",
                table: "Orders",
                column: "CostumerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Costumers_CostumerId",
                table: "Orders",
                column: "CostumerId",
                principalTable: "Costumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Costumers_CostumerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CostumerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CostumerId",
                table: "Orders");
        }
    }
}
