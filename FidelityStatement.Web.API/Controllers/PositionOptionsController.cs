using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FidelityStatement.Web.API.DAL;
using FidelityStatement.Web.API.DAL.Models;
using FidelityStatement.Web.API.DAL.DTO.Models;
using FidelityStatement.Web.API.DAL.Contracts;
using FidelityStatement.Web.API.DAL.Repositories;

namespace FidelityStatement.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionOptionsController : ControllerBase
    {
        private readonly FidelityStatementDbContext _context;
        private readonly IStockRepository _stockRepository;

        public PositionOptionsController(FidelityStatementDbContext context,
                                IStockRepository stockRepository)
        { 
            _context = context;
            _stockRepository = stockRepository;
        }

        // GET: api/PositionOptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionOption>>> GetPositionOptions()
        {
          if (_context.PositionOptions == null)
          {
              return NotFound();
          }
            return await _context.PositionOptions.ToListAsync();
        }

        // GET: api/PositionOptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PositionOption>> GetPositionOption(int id)
        {
          if (_context.PositionOptions == null)
          {
              return NotFound();
          }
            var positionOption = await _context.PositionOptions.FindAsync(id);

            if (positionOption == null)
            {
                return NotFound();
            }

            return positionOption;
        }

        // PUT: api/PositionOptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPositionOption(int id, PositionOption positionOption)
        {
            if (id != positionOption.Id)
            {
                return BadRequest();
            }

            _context.Entry(positionOption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionOptionExists(id))
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

        // POST: api/PositionOptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PositionOption>> PostPositionOption(PositionOption positionOption)
        {
          if (_context.PositionOptions == null)
          {
              return Problem("Entity set 'FidelityStatementDbContext.PositionOptions'  is null.");
          }

            var _positions = GetOptionTransactionPositions();

            foreach (var item in _positions.Result)
            {
                if (item.TransactionCount == 2 && IsOdd(item.PositionTypeId))
                {
                    var thisPosition = new PositionOption
                    {
                        StockSymbol = item.StockSymbol,
                        OptionType = item.OptionType,
                        StrikePrice = item.StrikePrice,
                        ExpirationDate = item.ExpirationDate,
                        StockId = _stockRepository.StockId(item.StockSymbol, item.UserUUID),
                        isSettled = true,
                        //Description = $"{item.StockSymbol} Closed {item.StrikePrice} {item.OptionType}",
                        BrokerageAccount = item.BrokerageAccount,
                        UserUUID = item.UserUUID,
                        Amount = item.TotalAmount
                    };
                    _context.PositionOptions.Add(thisPosition);
                    await _context.SaveChangesAsync();
                }
                if (item.TransactionCount == 1 &&   IsEven(item.PositionTypeId))        
                    {
                    var thisPosition = new PositionOption
                    {
                        StockSymbol = item.StockSymbol,
                        OptionType = item.OptionType,
                        StrikePrice = item.StrikePrice,
                        ExpirationDate = item.ExpirationDate,
                        StockId = _stockRepository.StockId(item.StockSymbol, item.UserUUID),
                        isSettled = false,
                        //Description = $"{item.StockSymbol} Closed {item.StrikePrice} {item.OptionType}",
                        BrokerageAccount = item.BrokerageAccount,
                        UserUUID = item.UserUUID,
                        Amount = item.TotalAmount
                    };
                    _context.PositionOptions.Add(thisPosition);
                    await _context.SaveChangesAsync();
                }
            }

            //_context.PositionOptions.Add(positionOption);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetPositionOption", new { id = positionOption.Id }, positionOption);
        }

        // DELETE: api/PositionOptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePositionOption(int id)
        {
            if (_context.PositionOptions == null)
            {
                return NotFound();
            }
            var positionOption = await _context.PositionOptions.FindAsync(id);
            if (positionOption == null)
            {
                return NotFound();
            }

            _context.PositionOptions.Remove(positionOption);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PositionOptionExists(int id)
        {
            return (_context.PositionOptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        private static bool IsOdd(int number)
        {
            return number % 2 != 0;
        }

        private async Task<List<PositionOptionDTO>> GetOptionTransactionPositions()
        {
            var query = from ot in _context.OptionTransactions
                        group ot by new
                        {
                            ot.StockSymbol,
                            ot.OptionSymbol,
                            ot.ExpirationDate,
                            ot.OptionType,
                            ot.StrikePrice,
                            ot.BrokerageAccount,
                            ot.UserUUID
                        } into grouped
                        select new PositionOptionDTO
                        {
                            StockSymbol = grouped.Key.StockSymbol,
                            OptionType = grouped.Key.OptionType,
                            StrikePrice = grouped.Key.StrikePrice,
                            ExpirationDate = grouped.Key.ExpirationDate,                                                        
                            OptionSymbol = grouped.Key.OptionSymbol,
                            BrokerageAccount = grouped.Key.BrokerageAccount,
                            UserUUID = grouped.Key.UserUUID,
                            //StockId = StockID(grouped.Key.StockSymbol, grouped.Key.UserUUID).Id,
                            TransactionCount = grouped.Count(),
                            PositionTypeId = grouped.Sum(g => g.PositionTypeId),
                            TotalAmount = grouped.Sum(g => g.Amount),
                        };

            return await query
                .OrderBy(result => result.ExpirationDate)
                .ThenBy(result => result.OptionSymbol)
                .ToListAsync();
        }

    }
}
