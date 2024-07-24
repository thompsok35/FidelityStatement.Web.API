using FidelityStatement.Web.API.DAL.Contracts;
using FidelityStatement.Web.API.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace FidelityStatement.Web.API.DAL.Repositories
{
    public class StockRepository : GenericRepository<Stock>, IStockRepository
    {
         private readonly FidelityStatementDbContext _dbContext;

        public StockRepository(FidelityStatementDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext; 
        }

        public int? StockId(string symbol, string user)
        {
            int result = 0;
            if (Exists(symbol, user))
            {
                var thisStock = _dbContext.Stocks.Where(x => x.StockSymbol == symbol && x.UserUUID == user).FirstOrDefault();
                return thisStock.Id;
            }
            return result;
        }

        public Stock AddNewStock(Stock stock)
        {
            var newStock = new Stock
            {
                StockSymbol = stock.StockSymbol,
                CompanyName = stock.CompanyName,
                BrokerageAccount = stock.BrokerageAccount,
                UserUUID = stock.UserUUID
            };
            _dbContext.Stocks.Add(newStock);
            _dbContext.SaveChanges();
            return newStock;
        }

        public bool Exists(string symbol, string user)
        {
            bool result = false;
            var thisStock = _dbContext.Stocks?.Where(e => e.StockSymbol == symbol && e.UserUUID == user).FirstOrDefault();
            if (thisStock != null)
            {
                return result;
            }
            return result;
        }
    }
}
