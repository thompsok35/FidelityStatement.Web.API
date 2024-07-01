using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidelityStatement.Web.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedtheOnModelCreatingtoforceeachrecordtobeuniqueinStockTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_AcquisitionType_StockSymbol_Quantity_Price_Commission_Fees_Amount_SettlementDate_BrokerageAccount_UserUUID",
                table: "StockTransactions",
                columns: new[] { "AcquisitionType", "StockSymbol", "Quantity", "Price", "Commission", "Fees", "Amount", "SettlementDate", "BrokerageAccount", "UserUUID" },
                unique: true,
                filter: "[Price] IS NOT NULL AND [Commission] IS NOT NULL AND [Fees] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockTransactions_AcquisitionType_StockSymbol_Quantity_Price_Commission_Fees_Amount_SettlementDate_BrokerageAccount_UserUUID",
                table: "StockTransactions");
        }
    }
}
