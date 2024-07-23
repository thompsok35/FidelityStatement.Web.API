using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidelityStatement.Web.API.Migrations
{
    /// <inheritdoc />
    public partial class addedpositionoptiontable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PerShareCostBasis",
                table: "Positions",
                type: "decimal(19,2)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PositionOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockSymbol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    OptionType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    StrikePrice = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    SettlementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BrokerageAccount = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserUUID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PositionUID = table.Column<int>(type: "int", nullable: false),
                    StockId = table.Column<int>(type: "int", nullable: false),
                    StrategyTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionOptions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PositionOptions");

            migrationBuilder.DropColumn(
                name: "PerShareCostBasis",
                table: "Positions");
        }
    }
}
