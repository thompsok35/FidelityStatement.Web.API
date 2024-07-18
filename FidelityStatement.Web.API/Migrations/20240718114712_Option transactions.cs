using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidelityStatement.Web.API.Migrations
{
    /// <inheritdoc />
    public partial class Optiontransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OptionTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PositionTypeId = table.Column<int>(type: "int", nullable: false),
                    StockSymbol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    OptionSymbol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OptionType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    StrategyTypeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(19,3)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    Commission = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    Fees = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    SettlementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrokerageAccount = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserUUID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionTransaction", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OptionTransaction");
        }
    }
}
