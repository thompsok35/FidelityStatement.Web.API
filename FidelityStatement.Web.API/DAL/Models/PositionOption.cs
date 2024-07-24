using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FidelityStatement.Web.API.DAL.Models
{
    public class PositionOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string? StockSymbol { get; set; }

        [Required]
        [MaxLength(5)]
        public string? OptionType { get; set; }

        [Column(TypeName = "decimal(19, 2)")]
        public decimal? StrikePrice { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }        

        [Required]
        [Column(TypeName = "decimal(19, 2)")]
        public decimal Amount { get; set; }

        [Required]
        public bool isSettled { get; set; } 

        public DateTime? SettlementDate { get; set; }

        [Required]
        [MaxLength(255)]
        public string? BrokerageAccount { get; set; }

        [Required]
        [MaxLength(255)]
        public string? UserUUID { get; set; }

        public int PositionUID { get; set; }

        public int? StockId { get; set; }
        public int StrategyTypeId { get; set; }
    }
}
