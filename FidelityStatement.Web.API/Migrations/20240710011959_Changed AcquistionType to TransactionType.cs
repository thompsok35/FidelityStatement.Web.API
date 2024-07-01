using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidelityStatement.Web.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAcquistionTypetoTransactionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockTransactions_AcquisitionType_StockSymbol_Quantity_Price_Commission_Fees_Amount_SettlementDate_BrokerageAccount_UserUUID",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "AcquisitionType",
                table: "StockTransactions");

            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "StockTransactions",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_TransactionType_StockSymbol_Quantity_Price_Commission_Fees_Amount_SettlementDate_BrokerageAccount_UserUUID",
                table: "StockTransactions",
                columns: new[] { "TransactionType", "StockSymbol", "Quantity", "Price", "Commission", "Fees", "Amount", "SettlementDate", "BrokerageAccount", "UserUUID" },
                unique: true,
                filter: "[Price] IS NOT NULL AND [Commission] IS NOT NULL AND [Fees] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockTransactions_TransactionType_StockSymbol_Quantity_Price_Commission_Fees_Amount_SettlementDate_BrokerageAccount_UserUUID",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "StockTransactions");

            migrationBuilder.AddColumn<string>(
                name: "AcquisitionType",
                table: "StockTransactions",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_AcquisitionType_StockSymbol_Quantity_Price_Commission_Fees_Amount_SettlementDate_BrokerageAccount_UserUUID",
                table: "StockTransactions",
                columns: new[] { "AcquisitionType", "StockSymbol", "Quantity", "Price", "Commission", "Fees", "Amount", "SettlementDate", "BrokerageAccount", "UserUUID" },
                unique: true,
                filter: "[Price] IS NOT NULL AND [Commission] IS NOT NULL AND [Fees] IS NOT NULL");
        }
    }
}
