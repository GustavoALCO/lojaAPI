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
            migrationBuilder.DropTable(
                name: "Attdata");

            migrationBuilder.AddColumn<string>(
                name: "AttDate_Assunto",
                table: "MarketCart",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "AttDate_Data",
                table: "MarketCart",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttDate_Assunto",
                table: "MarketCart");

            migrationBuilder.DropColumn(
                name: "AttDate_Data",
                table: "MarketCart");

            migrationBuilder.CreateTable(
                name: "Attdata",
                columns: table => new
                {
                    MarketCartId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Assunto = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attdata", x => new { x.MarketCartId, x.Id });
                    table.ForeignKey(
                        name: "FK_Attdata_MarketCart_MarketCartId",
                        column: x => x.MarketCartId,
                        principalTable: "MarketCart",
                        principalColumn: "MarketCartId",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
