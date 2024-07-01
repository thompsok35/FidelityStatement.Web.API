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

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        public int StockId { get; set; }

        [Column(TypeName = "decimal(19, 2)")]
        public decimal? CostBasis { get; set; }

        [Required]
        [MaxLength(255)]
        public string UserUUID { get; set; }
    }
}
