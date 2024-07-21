using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidelityStatement.Web.API.Migrations
{
    /// <inheritdoc />
    public partial class addedtablerelationshipstoposition : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "StockTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "OptionTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionTransaction_Positions_PositionId",
                table: "OptionTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_Positions_PositionId",
                table: "StockTransactions");

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "StockTransactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "OptionTransaction",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionTransaction_Positions_PositionId",
                table: "OptionTransaction",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_Positions_PositionId",
                table: "StockTransactions",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");
        }
    }
}
