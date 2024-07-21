using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FidelityStatement.Web.API.DAL.Models
{
    public class OptionTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string? TransactionType { get; set; }

        [Required]
        public int PositionTypeId { get; set; }

        public int PositionUID { get; set; }

        [Required]
        [MaxLength(10)]
        public string? StockSymbol { get; set; }

        [Required]
        [MaxLength(20)]
        public string? OptionSymbol { get; set; }

        [Required]
        [MaxLength(5)]
        public string? OptionType { get; set; }

        [Column(TypeName = "decimal(19, 2)")]
        public decimal? StrikePrice { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }
        
        public int StrategyTypeId { get; set; }

        [Required]
        [Column(TypeName = "decimal(19, 3)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(19, 2)")]
        public decimal? Price { get; set; }

        [Column(TypeName = "decimal(19, 2)")]
        public decimal? Commission { get; set; }

        [Column(TypeName = "decimal(19, 2)")]
        public decimal? Fees { get; set; }

        [Required]
        [Column(TypeName = "decimal(19, 2)")]
        public decimal Amount { get; set; }

        public DateTime? SettlementDate { get; set; }

        [Required]
        [MaxLength(255)]
        public string? BrokerageAccount { get; set; }

        [Required]
        [MaxLength(255)]
        public string? UserUUID { get; set; }

        ////References
        //[ForeignKey(nameof(PositionId))]
        //public int PositionId { get; set; }
        //public Position Position { get; set; }
    }
}
