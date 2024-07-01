using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidelityStatement.Web.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedStockTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockSymbol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(19,3)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    Commission = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    Fees = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    SettlementDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BrokerageAccount = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserUUID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransactions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockTransactions");
        }
    }
}
