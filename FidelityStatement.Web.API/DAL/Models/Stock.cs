using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FidelityStatement.Web.API.DAL.Models
{
    public class Stock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? CompanyName { get; set; }
        
        
        [Key]
        [MaxLength(10)]
        public string? StockSymbol { get; set; }

        [Required]
        [MaxLength(255)]
        public string? UserUUID { get; set; }

        [MaxLength(255)]
        public string? BrokerageAccount { get; set; }

        //[Column(TypeName = "decimal(19, 2)")]
        //public decimal? AquiredPrice { get; set; }

        //public DateTime? AquiredDate { get; set; }

    }
}
