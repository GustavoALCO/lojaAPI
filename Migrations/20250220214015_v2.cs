using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace loja_api.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdProducts",
                table: "MarketCart");

            migrationBuilder.CreateTable(
                name: "ProductsMarketCart",
                columns: table => new
                {
                    MarketCartId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdProducts = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsMarketCart", x => new { x.MarketCartId, x.IdProducts });
                    table.ForeignKey(
                        name: "FK_ProductsMarketCart_MarketCart_MarketCartId",
                        column: x => x.MarketCartId,
                        principalTable: "MarketCart",
                        principalColumn: "MarketCartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsMarketCart_Products_IdProducts",
                        column: x => x.IdProducts,
                        principalTable: "Products",
                        principalColumn: "IdProducts",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsMarketCart_IdProducts",
                table: "ProductsMarketCart",
                column: "IdProducts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsMarketCart");

            migrationBuilder.AddColumn<string>(
                name: "IdProducts",
                table: "MarketCart",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
