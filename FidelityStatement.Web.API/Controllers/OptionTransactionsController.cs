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
using System.Text.RegularExpressions;

namespace FidelityStatement.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionTransactionsController : ControllerBase
    {
        private readonly FidelityStatementDbContext _context;

        public OptionTransactionsController(FidelityStatementDbContext context)
        {
            _context = context;
        }

        // GET: api/OptionTransactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OptionTransaction>>> GetOptionTransaction()
        {
          if (_context.OptionTransaction == null)
          {
              return NotFound();
          }
            return await _context.OptionTransaction.ToListAsync();
        }

        // GET: api/OptionTransactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OptionTransaction>> GetOptionTransaction(int id)
        {
          if (_context.OptionTransaction == null)
          {
              return NotFound();
          }
            var optionTransaction = await _context.OptionTransaction.FindAsync(id);

            if (optionTransaction == null)
            {
                return NotFound();
            }

            return optionTransaction;
        }

        // PUT: api/OptionTransactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOptionTransaction(int id, OptionTransaction optionTransaction)
        {
            if (id != optionTransaction.Id)
            {
                return BadRequest();
            }

            _context.Entry(optionTransaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OptionTransactionExists(id))
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

        // POST: api/OptionTransactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OptionTransaction>> PostOptionTransaction(OptionTransaction optionTransaction)
        {
          if (_context.OptionTransaction == null)
          {
              return Problem("Entity set 'FidelityStatementDbContext.OptionTransaction'  is null.");
          }

            // Post the purchased Option contracts 
            var _Transactions = _context.Transactions.ToList();
            List<OptionTransaction> _optionsTransactions = new List<OptionTransaction>();
            foreach (var item in _Transactions)
            {
                try
                {
                    //Symbols > 10 characters are options
                    if (item.Symbol.Length >= 10)
                    {
                        //Action property analysis
                        var _sell = item.Action.Length >= 12 ? item.Action.Substring(0, 28) : string.Empty;                        
                        var _buy = item.Action.Length >= 12 ? item.Action.Substring(0, 30) : string.Empty;
                        var _expired = item.Action.Length >= 12 ? item.Action.Substring(0, 7) : string.Empty;
                        var _assigned = item.Action.Length >= 12 ? item.Action.Substring(0, 8) : string.Empty;

                        //Sell to Open = 20
                        if (_sell == "YOU SOLD OPENING TRANSACTION")
                        {
                            var thisTransaction = new OptionTransaction { 
                                TransactionType = "Sell",   
                                PositionTypeId = 20, 
                                OptionSymbol = item.Symbol.Replace("-",""),
                                StockSymbol = ExtractStockSymbol(item.Symbol),
                                ExpirationDate = ConvertToExpirationDate(ExtractExpirationDate(item.Symbol, ExtractStockSymbol(item.Symbol))),
                                StrikePrice = Convert.ToDecimal(ExtractStrikePrice(item.Symbol, ExtractStockSymbol(item.Symbol))),  
                                OptionType = ExtractOptionType(item.Symbol, ExtractStockSymbol(item.Symbol)),
                                Quantity = item.Quantity,
                                Price = item.Price,
                                Commission = item.Commission,
                                Fees = item.Fees,
                                Amount = item.Amount,
                                SettlementDate = StringToDateTime2(item.SettlementDate),
                                BrokerageAccount = item.BrokerageAccount,
                                UserUUID = item.UserUUID,
                            };
                            _optionsTransactions.Add(thisTransaction);
                        }
                        //Buy to Close = 11
                        if (_buy == "YOU BOUGHT CLOSING TRANSACTION")
                        {
                            var thisTransaction = new OptionTransaction
                            {
                                TransactionType = "Buy",
                                PositionTypeId = 11,
                                OptionSymbol = item.Symbol.Replace("-", ""),
                                StockSymbol = ExtractStockSymbol(item.Symbol),
                                ExpirationDate = ConvertToExpirationDate(ExtractExpirationDate(item.Symbol, ExtractStockSymbol(item.Symbol))),
                                StrikePrice = Convert.ToDecimal(ExtractStrikePrice(item.Symbol, ExtractStockSymbol(item.Symbol))),
                                OptionType = ExtractOptionType(item.Symbol, ExtractStockSymbol(item.Symbol)),
                                Quantity = item.Quantity,
                                Price = item.Price,
                                Commission = item.Commission,
                                Fees = item.Fees,
                                Amount = item.Amount,
                                SettlementDate = StringToDateTime2(item.SettlementDate),
                                BrokerageAccount = item.BrokerageAccount,
                                UserUUID = item.UserUUID,
                            };
                            _optionsTransactions.Add(thisTransaction);
                        }

                        //Buy to Open = 10
                        if (_buy == "YOU BOUGHT OPENING TRANSACTION")
                        {
                            var thisTransaction = new OptionTransaction
                            {
                                TransactionType = "Buy",
                                PositionTypeId = 10,
                                OptionSymbol = item.Symbol.Replace("-", ""),
                                StockSymbol = ExtractStockSymbol(item.Symbol),
                                ExpirationDate = ConvertToExpirationDate(ExtractExpirationDate(item.Symbol, ExtractStockSymbol(item.Symbol))),
                                StrikePrice = Convert.ToDecimal(ExtractStrikePrice(item.Symbol, ExtractStockSymbol(item.Symbol))),
                                OptionType = ExtractOptionType(item.Symbol, ExtractStockSymbol(item.Symbol)),
                                Quantity = item.Quantity,
                                Price = item.Price,
                                Commission = item.Commission,
                                Fees = item.Fees,
                                Amount = item.Amount,
                                SettlementDate = StringToDateTime2(item.SettlementDate),
                                BrokerageAccount = item.BrokerageAccount,
                                UserUUID = item.UserUUID,
                            };
                            _optionsTransactions.Add(thisTransaction);
                        }
                        //Sell to Close = 21
                        if (_sell == "YOU SOLD CLOSING TRANSACTION")
                        {
                            var thisTransaction = new OptionTransaction
                            {
                                TransactionType = "Sell",
                                PositionTypeId = 21,
                                OptionSymbol = item.Symbol.Replace("-", ""),
                                StockSymbol = ExtractStockSymbol(item.Symbol),
                                ExpirationDate = ConvertToExpirationDate(ExtractExpirationDate(item.Symbol, ExtractStockSymbol(item.Symbol))),
                                StrikePrice = Convert.ToDecimal(ExtractStrikePrice(item.Symbol, ExtractStockSymbol(item.Symbol))),
                                OptionType = ExtractOptionType(item.Symbol, ExtractStockSymbol(item.Symbol)),
                                Quantity = item.Quantity,
                                Price = item.Price,
                                Commission = item.Commission,
                                Fees = item.Fees,
                                Amount = item.Amount,
                                SettlementDate = StringToDateTime2(item.SettlementDate),
                                BrokerageAccount = item.BrokerageAccount,
                                UserUUID = item.UserUUID,
                            };
                            _optionsTransactions.Add(thisTransaction);
                        }

                        //Expired to Close = 41
                        if (_expired == "EXPIRED")
                        {
                            var thisTransaction = new OptionTransaction
                            {
                                TransactionType = "Expired",
                                PositionTypeId = 41,
                                OptionSymbol = item.Symbol.Replace("-", ""),
                                StockSymbol = ExtractStockSymbol(item.Symbol),
                                ExpirationDate = ConvertToExpirationDate(ExtractExpirationDate(item.Symbol, ExtractStockSymbol(item.Symbol))),
                                StrikePrice = Convert.ToDecimal(ExtractStrikePrice(item.Symbol, ExtractStockSymbol(item.Symbol))),
                                OptionType = ExtractOptionType(item.Symbol, ExtractStockSymbol(item.Symbol)),
                                Quantity = item.Quantity,
                                Price = item.Price,
                                Commission = item.Commission,
                                Fees = item.Fees,
                                Amount = item.Amount,
                                SettlementDate = StringToDateTime2(item.SettlementDate),
                                BrokerageAccount = item.BrokerageAccount,
                                UserUUID = item.UserUUID,
                            };
                            _optionsTransactions.Add(thisTransaction);
                        }
                        //Assigned to Close = 51
                        if (_assigned == "ASSIGNED")
                        {
                            var thisTransaction = new OptionTransaction
                            {
                                TransactionType = "Assigned",
                                PositionTypeId = 51,
                                OptionSymbol = item.Symbol.Replace("-", ""),
                                StockSymbol = ExtractStockSymbol(item.Symbol),
                                ExpirationDate = ConvertToExpirationDate(ExtractExpirationDate(item.Symbol, ExtractStockSymbol(item.Symbol))),
                                StrikePrice = Convert.ToDecimal(ExtractStrikePrice(item.Symbol, ExtractStockSymbol(item.Symbol))),
                                OptionType = ExtractOptionType(item.Symbol, ExtractStockSymbol(item.Symbol)),
                                Quantity = item.Quantity,
                                Price = item.Price,
                                Commission = item.Commission,
                                Fees = item.Fees,
                                Amount = item.Amount,
                                SettlementDate= StringToDateTime2(item.SettlementDate),
                                BrokerageAccount = item.BrokerageAccount,
                                UserUUID = item.UserUUID,
                            };
                            _optionsTransactions.Add(thisTransaction);
                        }   
                    }
                }
                catch (Exception message)
                {
                    //Unable to handle the current item
                    _optionsTransactions.Count();
                    Console.WriteLine(message);
                }

            }
            var soldOptions = _optionsTransactions; 

            _context.OptionTransaction.AddRange(_optionsTransactions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOptionTransaction", new { id = optionTransaction.Id }, optionTransaction);
        }

        // DELETE: api/OptionTransactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOptionTransaction(int id)
        {
            if (_context.OptionTransaction == null)
            {
                return NotFound();
            }
            var optionTransaction = await _context.OptionTransaction.FindAsync(id);
            if (optionTransaction == null)
            {
                return NotFound();
            }

            _context.OptionTransaction.Remove(optionTransaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OptionTransactionExists(int id)
        {
            return (_context.OptionTransaction?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        #region String Helpers
        private static DateTime ConvertToDateTime2(string dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
            {
                dateString = "01/01/1900";
            }
            string format = "MM/dd/yyyy"; // Format of the date string
            DateTime parsedDate = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
            return parsedDate;
        }

        private static DateTime? StringToDateTime2(string dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
            {
                return null;
            }
            string format = "MM/dd/yyyy"; // Format of the date string
            DateTime parsedDate = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
            return parsedDate;
        }

        private static DateTime ConvertToExpirationDate(string dateString)
        {            

            string yy = dateString.Substring(0, 2);
            string mm = dateString.Substring(2, 2);
            string dd = dateString.Substring(4, 2);
            string newDate = mm + "/" + dd + "/20" + yy;
            string format = "MM/dd/yyyy"; // Format of the date string
            DateTime parsedDate = DateTime.ParseExact(newDate, format, CultureInfo.InvariantCulture);

            return parsedDate;                        
        }

        private static string ExtractStockSymbol(string input)
        {
            input = input.Substring(0,7).Trim();
            // Check if the input starts with '-', if so, remove it
            if (input.StartsWith("-"))
            {
                input = input.Substring(1);
            }

            // Use regular expression to find all letters in the string
            MatchCollection matches = Regex.Matches(input, "[A-Za-z]");

            // Concatenate all found letters into the stockSymbol
            string stockSymbol = "";
            foreach (Match match in matches)
            {
                stockSymbol += match.Value;
            }

            return stockSymbol;
        }

        private static string ExtractOptionType(string input, string symbol)
        {
            input = input.Replace(symbol, "").Trim();
            // Check if the input starts with '-', if so, remove it
            if (input.StartsWith("-"))
            {
                input = input.Substring(1);
            }

            // Use regular expression to find all letters in the string
            MatchCollection matches = Regex.Matches(input, "[A-Za-z]");

            // Concatenate all found letters into the stockSymbol
            string optionType = "";
            foreach (Match match in matches)
            {
                optionType += match.Value;
            }
            if (optionType.ToLower() == "c")
            {
                optionType = "Call";
            }
            if (optionType.ToLower() == "p")
            {
                optionType = "Put";
            }
            return optionType;
        }

        private static string ExtractExpirationDate(string input, string symbol)
        {
            input = input.Replace(symbol, "").Trim();
            // Check if the input starts with '-', if so, remove it
            if (input.StartsWith("-"))
            {
                input = input.Substring(1);
            }

            // Use regular expression to find all letters in the string
            //MatchCollection matches = Regex.Matches(input, "[A-Za-z]");

            // Return the first 6 characters
            string expirationDate = input.Substring(0,6).Trim();            

            return expirationDate;
        }

        private static string ExtractStrikePrice(string input, string symbol)
        {
            var strikePrice = string.Empty;              

            // Check if the input starts with '-', if so, remove it
            if (input.StartsWith("-"))
            {
                input = input.Substring(1);
            }

            input = input.Replace(symbol, "").Trim();
            var xd = input.Substring(0,6).Trim();
            input = input.Replace(xd, "").Trim();

            // Use regular expression to find all letters in the string
            MatchCollection matches = Regex.Matches(input, "[A-Za-z]");
            foreach (Match match in matches)
            {
                strikePrice = input.Replace(match.Value,"");
            }
                        
            return strikePrice;
        }
        #endregion
    }
}
