using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidelityStatement.Web.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionUID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StockId = table.Column<int>(type: "int", nullable: false),
                    TotalShares = table.Column<int>(type: "int", nullable: false),
                    UnsettledOptions = table.Column<bool>(type: "bit", nullable: false),
                    TotalPnL = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    TotalDividends = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    TotalPremium = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    BrokerageAccount = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserUUID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PositionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionTypeId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    StockSymbol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserUUID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.StockSymbol);
                });

            migrationBuilder.CreateTable(
                name: "StrategyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionDescription = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StrategyTypeId = table.Column<int>(type: "int", nullable: false),
                    Legs = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrategyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ActionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RunDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Action = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(19,3)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    Commission = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    Fees = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    AccruedInterest = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    CashBalance = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    SettlementDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BrokerageAccount = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserUUID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    isProcessed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PositionTypeId = table.Column<int>(type: "int", nullable: false),
                    PositionUID = table.Column<int>(type: "int", nullable: false),
                    StockSymbol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    OptionSymbol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OptionType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    StrikePrice = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StrategyTypeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(19,3)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    Commission = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    Fees = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    SettlementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BrokerageAccount = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserUUID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionTransaction_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StockTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PositionTypeId = table.Column<int>(type: "int", nullable: false),
                    PositionUID = table.Column<int>(type: "int", nullable: false),
                    StockSymbol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(19,3)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    Commission = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    Fees = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    SettlementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrokerageAccount = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserUUID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransactions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ActionInstruction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Instruction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionResult = table.Column<bool>(type: "bit", nullable: true),
                    TransactionActionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionInstruction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionInstruction_TransactionActions_TransactionActionId",
                        column: x => x.TransactionActionId,
                        principalTable: "TransactionActions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionInstruction_TransactionActionId",
                table: "ActionInstruction",
                column: "TransactionActionId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionTransaction_PositionId",
                table: "OptionTransaction",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_PositionId",
                table: "StockTransactions",
                column: "PositionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionInstruction");

            migrationBuilder.DropTable(
                name: "OptionTransaction");

            migrationBuilder.DropTable(
                name: "PositionTypes");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "StockTransactions");

            migrationBuilder.DropTable(
                name: "StrategyTypes");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionActions");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
