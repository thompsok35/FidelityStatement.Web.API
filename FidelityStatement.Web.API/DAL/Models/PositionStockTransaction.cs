using System.ComponentModel.DataAnnotations.Schema;

namespace FidelityStatement.Web.API.DAL.Models
{
    public class PositionStockTransaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PositionId { get; set; }

        public int StockTransactionId { get; set; }

    }
}
