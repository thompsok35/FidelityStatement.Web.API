using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FidelityStatement.Web.API.DAL.DTO.Models
{
    public class PositionDTO
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }

        public int PositionUID { get; set; }

        //[MaxLength(255)]
        public string Name { get; set; }

        //[MaxLength(255)]
        public string? Description { get; set; }

        //[MaxLength(10)]
        public string? StockSymbol { get; set; }

        public int TotalShares { get; set; }

        //[Column(TypeName = "decimal(19, 2)")]
        public decimal? PerShareCostBasis { get; set; }

        public bool UnsettledOptions { get; set; }

        public bool isSettled { get; set; }

        //[Column(TypeName = "decimal(19, 2)")]
        public decimal? TotalPnL { get; set; }

        //[Column(TypeName = "decimal(19, 2)")]
        public decimal? TotalDividends { get; set; }

        //[Column(TypeName = "decimal(19, 2)")]
        public decimal? TotalPremium { get; set; }

        //[Required]
        //[MaxLength(255)]
        public string? BrokerageAccount { get; set; }

        //[Required]
        //[MaxLength(255)]
        public string? UserUUID { get; set; }

        public int TransactionCount { get; set; }
    }
}
