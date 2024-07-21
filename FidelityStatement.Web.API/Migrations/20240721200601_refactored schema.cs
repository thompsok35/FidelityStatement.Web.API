using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidelityStatement.Web.API.Migrations
{
    /// <inheritdoc />
    public partial class refactoredschema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionTransaction_Positions_PositionId",
                table: "OptionTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_Positions_PositionId",
                table: "StockTransactions");

            migrationBuilder.DropIndex(
                name: "IX_StockTransactions_PositionId",
                table: "StockTransactions");

            migrationBuilder.DropIndex(
                name: "IX_OptionTransaction_PositionId",
                table: "OptionTransaction");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "OptionTransaction");

            migrationBuilder.AddColumn<string>(
                name: "StockSymbol",
                table: "Positions",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockSymbol",
                table: "Positions");

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "StockTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StockId",
                table: "Positions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "OptionTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_PositionId",
                table: "StockTransactions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionTransaction_PositionId",
                table: "OptionTransaction",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionTransaction_Positions_PositionId",
                table: "OptionTransaction",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_Positions_PositionId",
                table: "StockTransactions",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
