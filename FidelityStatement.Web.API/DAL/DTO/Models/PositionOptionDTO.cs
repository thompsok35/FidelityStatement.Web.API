namespace FidelityStatement.Web.API.DAL.DTO.Models
{
    public class PositionOptionDTO
    {
        public int TransactionCount { get; set; } //

        public int PositionTypeId { get; set; }

        public int StockId { get; set; }

        public string? StockSymbol { get; set; }

        public string? OptionSymbol { get; set; }

        public string? OptionType { get; set; }

        public decimal? StrikePrice { get; set; }

        public DateTime ExpirationDate { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime? SettlementDate { get; set; }

        public string? BrokerageAccount { get; set; }

        public string? UserUUID { get; set; }

    }
}
