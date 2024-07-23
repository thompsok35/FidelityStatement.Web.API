using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FidelityStatement.Web.API.DAL.DTO.Models
{
    public class OptionTransactionPosition
    {
        //public int Id { get; set; }

        //public string? TransactionType { get; set; }

        public int PositionStatus { get; set; }

        public int TransactionCount { get; set; }

        public string? StockSymbol { get; set; }

        public string? OptionSymbol { get; set; }

        public string? OptionType { get; set; }

        public decimal? StrikePrice { get; set; }

        public DateTime ExpirationDate { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime? SettlementDate { get; set; }

        public string? BrokerageAccount { get; set; }

        public string? UserUUID { get; set; }

        //public int StrategyTypeId { get; set; }

        //public decimal Quantity { get; set; }

        //public decimal? Price { get; set; }

        //public decimal? Commission { get; set; }

        //public decimal? Fees { get; set; }

        

    }
}
