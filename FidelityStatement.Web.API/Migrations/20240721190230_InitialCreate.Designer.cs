﻿// <auto-generated />
using System;
using FidelityStatement.Web.API.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FidelityStatement.Web.API.Migrations
{
    [DbContext(typeof(FidelityStatementDbContext))]
    [Migration("20240721190230_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FidelityStatement.Web.API.DAL.Models.ActionInstruction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Instruction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("InstructionResult")
                        .HasColumnType("bit");

                    b.Property<int?>("TransactionActionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionActionId");

                    b.ToTable("ActionInstruction");
                });

            modelBuilder.Entity("FidelityStatement.Web.API.DAL.Models.OptionTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(19, 2)");

                    b.Property<string>("BrokerageAccount")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal?>("Commission")
                        .HasColumnType("decimal(19, 2)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Fees")
                        .HasColumnType("decimal(19, 2)");

                    b.Property<string>("OptionSymbol")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("OptionType")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.Property<int>("PositionTypeId")
                        .HasColumnType("int");

                    b.Property<int>("PositionUID")
                        .HasColumnType("int");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(19, 2)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(19, 3)");

                    b.Property<DateTime?>("SettlementDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StockSymbol")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("StrategyTypeId")
                        .HasColumnType("int");

                    b.Property<decimal?>("StrikePrice")
                        .HasColumnType("decimal(19, 2)");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UserUUID")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.ToTable("OptionTransaction");
                });

            modelBuilder.Entity("FidelityStatement.Web.API.DAL.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BrokerageAccount")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("PositionUID")
                        .HasColumnType("int");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalDividends")
                        .HasColumnType("decimal(19, 2)");

                    b.Property<decimal?>("TotalPnL")
                        .HasColumnType("decimal(19, 2)");

                    b.Property<decimal?>("TotalPremium")
                        .HasColumnType("decimal(19, 2)");

                    b.Property<int>("TotalShares")
                        .HasColumnType("int");

                    b.Property<bool>("UnsettledOptions")
                        .HasColumnType("bit");

                    b.Property<string>("UserUUID")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("FidelityStatement.Web.API.DAL.Models.PositionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<int>("PositionTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PositionTypes");
                });

            modelBuilder.Entity("FidelityStatement.Web.API.DAL.Models.Stock", b =>
                {
                    b.Property<string>("StockSymbol")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("UserUUID")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("StockSymbol");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("FidelityStatement.Web.API.DAL.Models.StockTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(19, 2)");

                    b.Property<string>("BrokerageAccount")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal?>("Commission")
                        .HasColumnType("decimal(19, 2)");

                    b.Property<decimal?>("Fees")
                        .HasColumnType("decimal(19, 2)");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.Property<int>("PositionTypeId")
                        .HasColumnType("int");

                    b.Property<int>("PositionUID")
                        .HasColumnType("int");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(19, 2)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(19, 3)");

                    b.Property<DateTime>("SettlementDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StockSymbol")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UserUUID")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.ToTable("StockTransactions");
                });

            modelBuilder.Entity("FidelityStatement.Web.API.DAL.Models.StrategyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ActionDescription")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Legs")
                        .HasColumnType("int");

                    b.Property<int>("StrategyTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("StrategyTypes");
                });

            modelBuilder.Entity("FidelityStatement.Web.API.DAL.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("AccruedInterest")
                        .HasColumnType("decimal(19, 2)");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(19, 2)");

                    b.Property<string>("BrokerageAccount")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("CashBalance")
                        .HasColumnType("decimal(19, 2)");

                    b.Property<decimal?>("Commission")
                        .HasColumnType("decimal(19, 2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal?>("Fees")
                        .HasColumnType("decimal(19, 2)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(19, 2)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(19, 3)");

                    b.Property<string>("RunDate")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SettlementDate")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserUUID")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("isProcessed")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("FidelityStatement.Web.API.DAL.Models.TransactionAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ActionDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("TransactionActions");
                });

            modelBuilder.Entity("FidelityStatement.Web.API.DAL.Models.ActionInstruction", b =>
                {
                    b.HasOne("FidelityStatement.Web.API.DAL.Models.TransactionAction", null)
                        .WithMany("ActionInstructions")
                        .HasForeignKey("TransactionActionId");
                });

            modelBuilder.Entity("FidelityStatement.Web.API.DAL.Models.OptionTransaction", b =>
                {
                    b.HasOne("FidelityStatement.Web.API.DAL.Models.Position", null)
                        .WithMany("OptionActivity")
                        .HasForeignKey("PositionId");
                });

            modelBuilder.Entity("FidelityStatement.Web.API.DAL.Models.StockTransaction", b =>
                {
                    b.HasOne("FidelityStatement.Web.API.DAL.Models.Position", null)
                        .WithMany("StockActivity")
                        .HasForeignKey("PositionId");
                });

            modelBuilder.Entity("FidelityStatement.Web.API.DAL.Models.Position", b =>
                {
                    b.Navigation("OptionActivity");

                    b.Navigation("StockActivity");
                });

            modelBuilder.Entity("FidelityStatement.Web.API.DAL.Models.TransactionAction", b =>
                {
                    b.Navigation("ActionInstructions");
                });
#pragma warning restore 612, 618
        }
    }
}