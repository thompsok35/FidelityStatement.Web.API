using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FidelityStatement.Web.API.DAL;
using FidelityStatement.Web.API.DAL.Models;
using Microsoft.SqlServer.Server;
using System.Globalization;

namespace FidelityStatement.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly FidelityStatementDbContext _context;

        public StocksController(FidelityStatementDbContext context)
        {
            _context = context;
        }

        // GET: api/Stocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetStocks()
        {
          if (_context.Stocks == null)
          {
              return NotFound();
          }
            return await _context.Stocks.ToListAsync();
        }

        // GET: api/Stocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> GetStock(int id)
        {
          if (_context.Stocks == null)
          {
              return NotFound();
          }
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return stock;
        }

        // PUT: api/Stocks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock(int id, Stock stock)
        {
            if (id != stock.Id)
            {
                return BadRequest();
            }

            _context.Entry(stock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(id))
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

        // POST: api/Stocks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Stock>> PostStock(Stock stock)
        {
          if (_context.Stocks == null)
          {
              return Problem("Entity set 'FidelityStatementDbContext.Stocks'  is null.");
          }
            // Returns just the purchase of stock shares transactions
            var stockTransactions = _context.Transactions.ToList();  
            List<Stock> newStocks = new List<Stock>();
            foreach (var item in stockTransactions)
            {
                // Assuming the Action string is at least 23 characters long
                var _buy = item.Action.Length >= 12 ? item.Action.Substring(0, 10) : string.Empty;
                var _sell = item.Action.Length >= 12 ? item.Action.Substring(0, 8) : string.Empty;
                var _open = item.Action.Length >= 32 ? item.Action.Substring(13, 19) : string.Empty;

                if (item.Symbol.Length <= 5)
                {
                    Console.WriteLine("New stock position");

                    if ( _buy == "YOU BOUGHT" | _sell == "YOU SOLD")
                    {
                        var thisStock = new Stock
                        {
                            CompanyName = item.Description.Replace('\\', ' ').Replace('"', ' ').Trim(),
                            StockSymbol = item.Symbol.Trim(),
                            BrokerageAccount = item.BrokerageAccount,
                            UserUUID = item.UserUUID
                        };
                        // Only add Stocks that are not already stored in the table
                        if (!newStocks.Any(s => s.StockSymbol.Trim() == item.Symbol.Trim())
                            && (!_context.Stocks?.Any(e => e.StockSymbol == item.Symbol.Trim())).GetValueOrDefault()
                            )
                        {
                            newStocks.Add(thisStock);
                        }
                    }
                }
            }

            _context.Stocks.AddRange(newStocks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStock", new { id = stock.Id }, stock);
        }

        // DELETE: api/Stocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            if (_context.Stocks == null)
            {
                return NotFound();
            }
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool StockExists(int id)
        {
            return (_context.Stocks?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        static DateTime ConvertToDateTime2(string dateString)
        {
            string format = "MM/dd/yyyy"; // Format of the date string
            DateTime parsedDate = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
            return parsedDate;
        }

    }
}
