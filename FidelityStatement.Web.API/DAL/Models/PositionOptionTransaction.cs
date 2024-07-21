using System.ComponentModel.DataAnnotations.Schema;

namespace FidelityStatement.Web.API.DAL.Models
{
    public class PositionOptionTransaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PositionId { get; set; }

        public int OptionTransactionId { get; set; }
    }
}
