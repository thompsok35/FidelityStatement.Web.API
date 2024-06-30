namespace FidelityStatement.Web.API.DAL.Models
{
    public class Position
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string? UnderlyingSymbol { get; set; }
        public int SymbolId { get; set; }
        public double YTDProfitLoss { get; set; }


        //public virtual IList<Transaction> Transactions { get; set; }
    }
}
