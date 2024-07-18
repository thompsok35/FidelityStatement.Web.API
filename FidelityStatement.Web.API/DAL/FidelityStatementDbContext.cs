using FidelityStatement.Web.API.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FidelityStatement.Web.API.DAL
{
    public partial class FidelityStatementDbContext : DbContext
    {
        public FidelityStatementDbContext(DbContextOptions<FidelityStatementDbContext> options) : base(options)
        {

        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<TransactionAction> TransactionActions { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<OptionTransaction> OptionTransactions { get; set; }    
        public DbSet<PositionType> PositionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockTransaction>(entity =>
            {
                entity.HasIndex(st => new
                 {
                     st.TransactionType,
                     st.StockSymbol,
                     st.Quantity,
                     st.Price,
                     st.Commission,
                     st.Fees,
                     st.Amount,
                     st.SettlementDate,
                     st.BrokerageAccount,
                     st.UserUUID
                 })
                .IsUnique();

                entity.Property(st => st.TransactionType)
                .IsRequired()
                .HasMaxLength(20);

                entity.Property(st => st.StockSymbol)
                .IsRequired()
                .HasMaxLength(10);

                entity.Property(st => st.Quantity)
                .IsRequired()
                .HasColumnType("decimal(19, 3)");

                entity.Property(st => st.Price)
                .HasColumnType("decimal(19, 2)");

                entity.Property(st => st.Commission)
                .HasColumnType("decimal(19, 2)");

                entity.Property(st => st.Fees)
                .HasColumnType("decimal(19, 2)");

                entity.Property(st => st.Amount)
                .IsRequired()
                .HasColumnType("decimal(19, 2)");

                entity.Property(st => st.SettlementDate)
                .IsRequired();

                entity.Property(st => st.BrokerageAccount)
                .IsRequired()
                .HasMaxLength(255);

                entity.Property(st => st.UserUUID)
                .IsRequired()
                .HasMaxLength(255);


            });


            //modelBuilder.Entity<StockTransaction>()
            //    .HasIndex(st => new
            //    {
            //        st.AcquisitionType,
            //        st.StockSymbol,
            //        st.Quantity,
            //        st.Price,
            //        st.Commission,
            //        st.Fees,
            //        st.Amount,
            //        st.SettlementDate,
            //        st.BrokerageAccount,
            //        st.UserUUID
            //    })
            //    .IsUnique();

            //modelBuilder.Entity<StockTransaction>()
            //    .Property(st => st.AcquisitionType)
            //    .IsRequired()
            //    .HasMaxLength(10);

            //modelBuilder.Entity<StockTransaction>()
            //    .Property(st => st.StockSymbol)
            //    .IsRequired()
            //    .HasMaxLength(10);

            //modelBuilder.Entity<StockTransaction>()
            //    .Property(st => st.Quantity)
            //    .IsRequired()
            //    .HasColumnType("decimal(19, 3)");

            //modelBuilder.Entity<StockTransaction>()
            //    .Property(st => st.Price)
            //    .HasColumnType("decimal(19, 2)");

            //modelBuilder.Entity<StockTransaction>()
            //    .Property(st => st.Commission)
            //    .HasColumnType("decimal(19, 2)");

            //modelBuilder.Entity<StockTransaction>()
            //    .Property(st => st.Fees)
            //    .HasColumnType("decimal(19, 2)");

            //modelBuilder.Entity<StockTransaction>()
            //    .Property(st => st.Amount)
            //    .IsRequired()
            //    .HasColumnType("decimal(19, 2)");

            //modelBuilder.Entity<StockTransaction>()
            //    .Property(st => st.SettlementDate)
            //    .IsRequired();

            //modelBuilder.Entity<StockTransaction>()
            //    .Property(st => st.BrokerageAccount)
            //    .IsRequired()
            //    .HasMaxLength(255);

            //modelBuilder.Entity<StockTransaction>()
            //    .Property(st => st.UserUUID)
            //    .IsRequired()
            //    .HasMaxLength(255);

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        public DbSet<FidelityStatement.Web.API.DAL.Models.OptionTransaction> OptionTransaction { get; set; } = default!;
    }
}
