using FidelityStatement.Web.API.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FidelityStatement.Web.API.DAL
{
    public class FidelityStatementDbContext : DbContext
    {
        public FidelityStatementDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
}
