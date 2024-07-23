using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FidelityStatement.Web.API.DAL.Models
{
    public class Position
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PositionUID { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        [MaxLength(10)]
        public string? StockSymbol { get; set; }

        public int TotalShares { get; set; }

        [Column(TypeName = "decimal(19, 2)")]
        public decimal? PerShareCostBasis { get; set; }

        public bool UnsettledOptions { get; set; }

        [Column(TypeName = "decimal(19, 2)")]
        public decimal? TotalPnL { get; set; }

        [Column(TypeName = "decimal(19, 2)")]
        public decimal? TotalDividends { get; set; }

        [Column(TypeName = "decimal(19, 2)")]
        public decimal? TotalPremium { get; set; }

        [Required]
        [MaxLength(255)]
        public string? BrokerageAccount { get; set; }

        [Required]
        [MaxLength(255)]
        public string? UserUUID { get; set; }


        ////Position has many Option and Stock transaction activities
        //public virtual List<OptionTransaction>? OptionTransactionActivity { get; set; }  

        //public virtual List<StockTransaction>? StockTransactionActivity { get; set; }

    }
}
