using FidelityStatement.Web.API.DAL.Models;

namespace FidelityStatement.Web.API.DAL.Contracts
{
    public interface IStockRepository
    {
        int? StockId(string symbol, string user);

        bool Exists(string symbol, string user);

        public Stock AddNewStock(Stock stock);
    }
}
