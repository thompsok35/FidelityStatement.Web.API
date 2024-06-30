using System.ComponentModel.DataAnnotations.Schema;

namespace FidelityStatement.Web.API.DAL.Models
{
    public class Transaction
    {
        public int Id { get; set; }  // Primary key
        public DateTime RunDate { get; set; }
        public string? Action { get; set; }
        public string? Symbol { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public decimal Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Commission { get; set; }
        public decimal? Fees { get; set; }
        public decimal? AccruedInterest { get; set; }
        public decimal Amount { get; set; }
        public decimal CashBalance { get; set; }
        public DateTime? SettlementDate { get; set; }

        //[ForeignKey(nameof(PositionId))]
        //public int PositionId { get; set; }
        //public Position? Position { get; set; } 
    }
}
