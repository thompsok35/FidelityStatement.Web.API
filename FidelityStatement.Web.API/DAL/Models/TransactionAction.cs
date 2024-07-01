using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FidelityStatement.Web.API.DAL.Models
{
    public class TransactionAction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
                
        [Required]
        [MaxLength(255)]
        public string? Action { get; set; }

        public string? ActionDescription { get; set; }

        public List<ActionInstruction>? ActionInstructions { get; set; }

        public DateTime CreatedDate { get; set; }

    }

    public class ActionInstruction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Instruction { get; set; }

        public bool? InstructionResult { get; set; }
    }


}
