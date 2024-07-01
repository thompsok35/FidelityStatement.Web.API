using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FidelityStatement.Web.API.DAL;
using FidelityStatement.Web.API.DAL.Models;
using System.Globalization;
using Azure.Core;
using Microsoft.SqlServer.Server;

namespace FidelityStatement.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly FidelityStatementDbContext _context;

        public TransactionsController(FidelityStatementDbContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
          if (_context.Transactions == null)
          {
              return NotFound();
          }
            return await _context.Transactions.ToListAsync();
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
          if (_context.Transactions == null)
          {
              return NotFound();
          }
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
          if (_context.Transactions == null)
          {
              return Problem("Entity set 'FidelityStatementDbContext.Transactions'  is null.");
          }
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
        }


        // POST: api/uploadfile
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("uploadfile")]
        //public async Task<IActionResult> PostUploadFile([FromForm] IFormFile file)
        public async Task<ActionResult<string>> PostUploadFile(string filePath)
        {
            int _errorLines = 0;
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return BadRequest("File path cannot be null or empty.");
            }

            
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }
            else
            {
                var fileContent = await System.IO.File.ReadAllTextAsync(filePath);
                //int x = fileContent.count();
                var transactions = new List<Transaction>();

                using (var stream = new StreamReader(filePath))
                {
                    //string headerLine = await stream.ReadLineAsync().Result; // Read header line
                    int _blankLines = 0;
                    int _dataLines = 0;
                    
                    while (!stream.EndOfStream)
                    {
                        try
                        {
                            var line = await stream.ReadLineAsync();
                            if (line != "")
                            {
                                var values = line.Split(',');
                                if (values[0] != "Run Date" || values[0].Trim().Count() < 9)
                                {
                                    var transaction = new Transaction
                                    {
                                        RunDate = values[0],
                                        Action = values[1],
                                        Symbol = values[2],
                                        Description = values[3],
                                        Type = values[4],
                                        Quantity = decimal.Parse(values[5]),
                                        Price = string.IsNullOrWhiteSpace(values[6]) ? (decimal?)null : decimal.Parse(values[6]),
                                        Commission = string.IsNullOrWhiteSpace(values[7]) ? (decimal?)null : decimal.Parse(values[7]),
                                        Fees = string.IsNullOrWhiteSpace(values[8]) ? (decimal?)null : decimal.Parse(values[8]),
                                        AccruedInterest = string.IsNullOrWhiteSpace(values[9]) ? (decimal?)null : decimal.Parse(values[9]),
                                        Amount = decimal.Parse(values[10]),
                                        CashBalance = decimal.Parse(values[11]),
                                        SettlementDate = values[12]
                                    };
                                    transactions.Add(transaction);
                                    _dataLines++;
                                }
                            }
                            else
                            {
                                _blankLines++;
                            }

                            if (_blankLines > 4)
                            {
                                var x = stream.Close;
                            }
                        }
                        catch (Exception Message)
                        {
                            _errorLines++;
                            var message = Message.Message;
                        }
                    }
                    
                }
                
                _context.Transactions.AddRange(transactions);
                await _context.SaveChangesAsync();

                return Ok(new { count = transactions.Count });
                //return Ok(new { count = _errorLines });
            }
        }

        private bool ValidateFileExists(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }

            return System.IO.File.Exists(filePath);
        }

        private IEnumerable<string> ReadFileLines(string filePath)
        {
            if (!ValidateFileExists(filePath))
            {
                throw new FileNotFoundException("File not found.", filePath);
            }

            return System.IO.File.ReadLines(filePath);
        }


        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            if (_context.Transactions == null)
            {
                return NotFound();
            }
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(int id)
        {
            return (_context.Transactions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
