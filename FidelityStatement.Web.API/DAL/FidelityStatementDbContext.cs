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
        public DbSet<PositionOption> PositionOptions { get; set; }
        public DbSet<PositionOptionTransaction> PositionOptionTransactions { get; set; }
        public DbSet<PositionStockTransaction> PositionStockTransactions { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<TransactionAction> TransactionActions { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<OptionTransaction> OptionTransactions { get; set; }    
        public DbSet<PositionType> PositionTypes { get; set; }
        public DbSet<StrategyType> StrategyTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                      

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        public DbSet<FidelityStatement.Web.API.DAL.Models.OptionTransaction> OptionTransaction { get; set; } = default!;
    }
}
