using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportCompanies.Migrations
{
    /// <inheritdoc />
    public partial class InsertingOwnedTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Costumers_CostumerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Statuses_StatusId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_TransportCompanies_TransportCompanyId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "TransportCompanyId",
                table: "Orders",
                newName: "transportCompanyId");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Orders",
                newName: "statusID");

            migrationBuilder.RenameColumn(
                name: "CostumerId",
                table: "Orders",
                newName: "costumerId");

            migrationBuilder.RenameColumn(
                name: "orderedItens",
                table: "Orders",
                newName: "Origin_Cep");

            migrationBuilder.RenameColumn(
                name: "Origin",
                table: "Orders",
                newName: "Destination_Cep");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_TransportCompanyId",
                table: "Orders",
                newName: "IX_Orders_transportCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_StatusId",
                table: "Orders",
                newName: "IX_Orders_statusID");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CostumerId",
                table: "Orders",
                newName: "IX_Orders_costumerId");

            migrationBuilder.AddColumn<string>(
                name: "Destination_Complement",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Destination_Number",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Origin_Complement",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Origin_Number",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ItemDto",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDto", x => new { x.OrderId, x.Id });
                    table.ForeignKey(
                        name: "FK_ItemDto_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Costumers_costumerId",
                table: "Orders",
                column: "costumerId",
                principalTable: "Costumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Statuses_statusID",
                table: "Orders",
                column: "statusID",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_TransportCompanies_transportCompanyId",
                table: "Orders",
                column: "transportCompanyId",
                principalTable: "TransportCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Costumers_costumerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Statuses_statusID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_TransportCompanies_transportCompanyId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "ItemDto");

            migrationBuilder.DropColumn(
                name: "Destination_Complement",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Destination_Number",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Origin_Complement",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Origin_Number",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "transportCompanyId",
                table: "Orders",
                newName: "TransportCompanyId");

            migrationBuilder.RenameColumn(
                name: "statusID",
                table: "Orders",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "costumerId",
                table: "Orders",
                newName: "CostumerId");

            migrationBuilder.RenameColumn(
                name: "Origin_Cep",
                table: "Orders",
                newName: "orderedItens");

            migrationBuilder.RenameColumn(
                name: "Destination_Cep",
                table: "Orders",
                newName: "Origin");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_transportCompanyId",
                table: "Orders",
                newName: "IX_Orders_TransportCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_statusID",
                table: "Orders",
                newName: "IX_Orders_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_costumerId",
                table: "Orders",
                newName: "IX_Orders_CostumerId");

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Costumers_CostumerId",
                table: "Orders",
                column: "CostumerId",
                principalTable: "Costumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Statuses_StatusId",
                table: "Orders",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_TransportCompanies_TransportCompanyId",
                table: "Orders",
                column: "TransportCompanyId",
                principalTable: "TransportCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
