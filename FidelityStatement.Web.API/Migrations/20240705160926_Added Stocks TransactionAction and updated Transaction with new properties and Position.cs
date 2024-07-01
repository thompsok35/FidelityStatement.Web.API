using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidelityStatement.Web.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedStocksTransactionActionandupdatedTransactionwithnewpropertiesandPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SymbolId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "UnderlyingSymbol",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "YTDProfitLoss",
                table: "Positions");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Positions",
                newName: "StockId");

            migrationBuilder.AddColumn<string>(
                name: "BrokerageAccount",
                table: "Transactions",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserUUID",
                table: "Transactions",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isProcessed",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "CostBasis",
                table: "Positions",
                type: "decimal(19,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Positions",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Positions",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserUUID",
                table: "Positions",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrokerageAccount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UserUUID",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "isProcessed",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CostBasis",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "UserUUID",
                table: "Positions");

            migrationBuilder.RenameColumn(
                name: "StockId",
                table: "Positions",
                newName: "Year");

            migrationBuilder.AddColumn<int>(
                name: "SymbolId",
                table: "Positions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnderlyingSymbol",
                table: "Positions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "YTDProfitLoss",
                table: "Positions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
