using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace loja_api.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cupom",
                columns: table => new
                {
                    CupomId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Discount = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Auditable_CreatebyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Auditable_CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Auditable_UpdatebyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Auditable_UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupom", x => x.CupomId);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Login = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Position = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Auditable_CreatebyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Auditable_CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Auditable_UpdatebyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Auditable_UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    IdProducts = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    ProductDescription = table.Column<string>(type: "TEXT", nullable: false),
                    CodeProduct = table.Column<string>(type: "TEXT", nullable: false),
                    TypeProduct = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    QuantityStorage = table.Column<int>(type: "INTEGER", nullable: false),
                    Auditable_CreatebyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Auditable_CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Auditable_UpdatebyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Auditable_UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.IdProducts);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    Cpf = table.Column<string>(type: "TEXT", nullable: false),
                    Cep = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Storage",
                columns: table => new
                {
                    IdStorage = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdProducts = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceBuy = table.Column<double>(type: "REAL", nullable: false),
                    Auditable_CreatebyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Auditable_CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Auditable_UpdatebyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Auditable_UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storage", x => x.IdStorage);
                    table.ForeignKey(
                        name: "FK_Storage_Products_IdProducts",
                        column: x => x.IdProducts,
                        principalTable: "Products",
                        principalColumn: "IdProducts",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarketCart",
                columns: table => new
                {
                    MarketCartId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CupomId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    AttDate = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketCart", x => x.MarketCartId);
                    table.ForeignKey(
                        name: "FK_MarketCart_Cupom_CupomId",
                        column: x => x.CupomId,
                        principalTable: "Cupom",
                        principalColumn: "CupomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketCart_Users_MarketCartId",
                        column: x => x.MarketCartId,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_MarketCart_CupomId",
                table: "MarketCart",
                column: "CupomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductsMarketCart_IdProducts",
                table: "ProductsMarketCart",
                column: "IdProducts");

            migrationBuilder.CreateIndex(
                name: "IX_Storage_IdProducts",
                table: "Storage",
                column: "IdProducts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "ProductsMarketCart");

            migrationBuilder.DropTable(
                name: "Storage");

            migrationBuilder.DropTable(
                name: "MarketCart");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Cupom");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
