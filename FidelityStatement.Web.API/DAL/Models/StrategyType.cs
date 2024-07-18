using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FidelityStatement.Web.API.DAL.Models
{
    public class StrategyType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string? ActionDescription { get; set; }

        [Required]
        public int StrategyTypeId { get; set; }

        [Required]
        public int Legs { get; set; }
    }
}
