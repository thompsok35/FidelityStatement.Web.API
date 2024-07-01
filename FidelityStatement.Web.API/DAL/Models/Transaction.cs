using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FidelityStatement.Web.API.DAL.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string? RunDate { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Action { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Symbol { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Type { get; set; }

        [Required]
        [Column(TypeName = "decimal(19, 3)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(19, 2)")]
        public decimal? Price { get; set; }

        [Column(TypeName = "decimal(19, 2)")]
        public decimal? Commission { get; set; }

        [Column(TypeName = "decimal(19, 2)")]
        public decimal? Fees { get; set; }

        [Column(TypeName = "decimal(19, 2)")]
        public decimal? AccruedInterest { get; set; }

        [Required]
        [Column(TypeName = "decimal(19, 2)")]
        public decimal Amount { get; set; }

        [Required]
        [Column(TypeName = "decimal(19, 2)")]
        public decimal CashBalance { get; set; }

        [MaxLength(50)]
        public string? SettlementDate { get; set; }

    }
}
