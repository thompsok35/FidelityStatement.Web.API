using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FidelityStatement.Web.API.DAL.Models
{
    public class PositionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PositionTypeId { get; set; }

        [Required]
        [MaxLength(16)]
        public string? Action { get; set; }

        [Required]
        [MaxLength(16)]
        public string? Position { get; set; }
    }
}
