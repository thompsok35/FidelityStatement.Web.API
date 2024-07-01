using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FidelityStatement.Web.API.DAL;
using FidelityStatement.Web.API.DAL.Models;
using System.Globalization;

namespace FidelityStatement.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockTransactionsController : ControllerBase
    {
        private readonly FidelityStatementDbContext _context;

        public StockTransactionsController(FidelityStatementDbContext context)
        {
            _context = context;
        }

        // GET: api/StockTransactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockTransaction>>> GetStockTransactions()
        {
          if (_context.StockTransactions == null)
          {
              return NotFound();
          }
            return await _context.StockTransactions.ToListAsync();
        }

        // GET: api/StockTransactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockTransaction>> GetStockTransaction(int id)
        {
          if (_context.StockTransactions == null)
          {
              return NotFound();
          }
            var stockTransaction = await _context.StockTransactions.FindAsync(id);

            if (stockTransaction == null)
            {
                return NotFound();
            }

            return stockTransaction;
        }

        // PUT: api/StockTransactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockTransaction(int id, StockTransaction stockTransaction)
        {
            if (id != stockTransaction.Id)
            {
                return BadRequest();
            }

            _context.Entry(stockTransaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockTransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StockTransactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockTransaction>> PostStockTransaction(StockTransaction stockTransaction)
        {
          if (_context.StockTransactions == null)
          {
              return Problem("Entity set 'FidelityStatementDbContext.StockTransactions'  is null.");
          }

            // Post the purchased and Put Assigned shares of stock 
            var stockTransactions = _context.Transactions.ToList();
            List<StockTransaction> newStockTransactions = new List<StockTransaction>();
            foreach (var item in stockTransactions)
            {
                // Assuming the Action string is at least 23 characters long
                var _buy = item.Action.Length >= 12 ? item.Action.Substring(2, 10) : string.Empty;
                var _toOpen = item.Action.Length >= 32 ? item.Action.Substring(13, 19) : string.Empty;
                var _assignedShares = item.Action.Length >= 26 ? item.Action.Substring(2, 24) : string.Empty;
                if (_buy == "YOU BOUGHT" | _assignedShares == "YOU BOUGHT ASSIGNED PUTS")
                {
                    var _AcquisitionType = String.Empty;
                    //Console.WriteLine("New stock position");
                    if (_buy == "YOU BOUGHT") {_AcquisitionType = "Buy";}
                    if (_assignedShares == "YOU BOUGHT ASSIGNED PUTS") { _AcquisitionType = "Assigned-Put"; }
                    if (item.Symbol.Length <= 5)
                    {
                        var thisStockTransaction = new StockTransaction
                        {
                            //CompanyName = item.Description.Replace('\\', ' ').Replace('"', ' ').Trim(),
                            StockSymbol = item.Symbol.Trim(),
                            Quantity = item.Quantity,
                            Price = item.Price,
                            Commission = item.Commission,
                            Fees = item.Fees,
                            Amount = item.Amount,
                            SettlementDate = ConvertToDateTime2(item.SettlementDate),
                            BrokerageAccount = item.BrokerageAccount,
                            UserUUID = item.UserUUID,
                            TransactionType = _AcquisitionType
                        };

                        //newStockTransactions.Add(thisStockTransaction);

                        try
                        {
                            var thisRecordExists = _context.StockTransactions.Where(x => x.StockSymbol == thisStockTransaction.StockSymbol && x.SettlementDate == thisStockTransaction.SettlementDate).FirstOrDefault();
                            // Only add Stock transactions that are not already stored in the table
                            if (thisRecordExists == null)
                            {
                                _context.StockTransactions.Add(thisStockTransaction);
                                await _context.SaveChangesAsync();
                            }

                        }
                        catch (Exception message)
                        {
                            Console.WriteLine(message.Message);
                        }
                    }
                }
            }

            // Post the sold and Call-Assigned shares of stock 
            stockTransactions.Clear();
            stockTransactions = _context.Transactions.ToList();
            List<StockTransaction> soldStockTransactions = new List<StockTransaction>();
            foreach (var item in stockTransactions)
            {
                // Assuming the Action string is at least 23 characters long
                var _sell = item.Action.Length >= 12 ? item.Action.Substring(2, 8) : string.Empty;
                var _toOpen = item.Action.Length >= 32 ? item.Action.Substring(13, 19) : string.Empty;
                var _assignedShares = item.Action.Length >= 26 ? item.Action.Substring(2, 23) : string.Empty;
                if (_sell == "YOU SOLD" | _assignedShares == "YOU SOLD ASSIGNED CALLS")
                {
                    var _AcquisitionType = String.Empty;
                    //Console.WriteLine("New stock position");
                    if (_sell == "YOU SOLD") { _AcquisitionType = "Sell"; }
                    if (_assignedShares == "YOU SOLD ASSIGNED CALLS") { _AcquisitionType = "Assigned-Call"; }
                    if (item.Symbol.Length <= 5)
                    {
                        var thisStockTransaction = new StockTransaction
                        {
                            //CompanyName = item.Description.Replace('\\', ' ').Replace('"', ' ').Trim(),
                            StockSymbol = item.Symbol.Trim(),
                            Quantity = item.Quantity,
                            Price = item.Price,
                            Commission = item.Commission,
                            Fees = item.Fees,
                            Amount = item.Amount,
                            SettlementDate = ConvertToDateTime2(item.SettlementDate),
                            BrokerageAccount = item.BrokerageAccount,
                            UserUUID = item.UserUUID,
                            TransactionType = _AcquisitionType
                        };

                        //newStockTransactions.Add(thisStockTransaction);

                        try
                        {
                            var thisRecordExists = _context.StockTransactions.Where(x => x.StockSymbol == thisStockTransaction.StockSymbol && x.SettlementDate == thisStockTransaction.SettlementDate).FirstOrDefault();
                            // Only add Stock transactions that are not already stored in the table
                            if (thisRecordExists == null)
                            {
                                _context.StockTransactions.Add(thisStockTransaction);
                                await _context.SaveChangesAsync();
                            }

                        }
                        catch (Exception message)
                        {
                            Console.WriteLine(message.Message);
                        }


                    }
                }
            }

            return CreatedAtAction("GetStockTransaction", new { id = stockTransaction.Id }, stockTransaction);
        }

        // DELETE: api/StockTransactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockTransaction(int id)
        {
            if (_context.StockTransactions == null)
            {
                return NotFound();
            }
            var stockTransaction = await _context.StockTransactions.FindAsync(id);
            if (stockTransaction == null)
            {
                return NotFound();
            }

            _context.StockTransactions.Remove(stockTransaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockTransactionExists(int id)
        {
            return (_context.StockTransactions?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        static DateTime ConvertToDateTime2(string dateString)
        {
            string format = "MM/dd/yyyy"; // Format of the date string
            DateTime parsedDate = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
            return parsedDate;
        }

    }
}
