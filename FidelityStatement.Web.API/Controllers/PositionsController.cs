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

namespace FidelityStatement.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly FidelityStatementDbContext _context;
        private readonly IStockRepository _stockRepository;

        public PositionsController(FidelityStatementDbContext context,
                                IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }

        // GET: api/Positions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions()
        {
          if (_context.Positions == null)
          {
              return NotFound();
          }
            return await _context.Positions.ToListAsync();
        }

        // GET: api/Positions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> GetPosition(int id)
        {
          if (_context.Positions == null)
          {
              return NotFound();
          }
            var position = await _context.Positions.FindAsync(id);

            if (position == null)
            {
                return NotFound();
            }

            return position;
        }

        // PUT: api/Positions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosition(int id, Position position)
        {
            if (id != position.Id)
            {
                return BadRequest();
            }

            _context.Entry(position).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionExists(id))
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

        // POST: api/Positions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Position>> PostPosition(Position position)
        {
          if (_context.Positions == null)
          {
              return Problem("Entity set 'FidelityStatementDbContext.Positions'  is null.");
          }
            var positionClosedOptions = await GetClosedPositionOptions(); 
            var positionOpenOptions = await GetOpenPositionOptions();   

            if (positionClosedOptions != null)
            {
                foreach (var positionOption in positionClosedOptions)
                {
                    if (StockPositionExists(positionOption.StockSymbol))
                    {
                        var thisPosition = new Position                         
                        { 
                            Name = positionOption.Name, 
                            StockSymbol = positionOption.StockSymbol,
                            
                            TotalPremium = positionOption.TotalPremium,
                            BrokerageAccount = positionOption.BrokerageAccount,
                            UserUUID = positionOption.UserUUID
                        };
                        _context.Positions.Add(thisPosition);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        //Add the Stock to the Stock table
                        var thisStock = new Stock
                        {
                            StockSymbol = positionOption.StockSymbol,
                            CompanyName = positionOption.Name,
                            BrokerageAccount = positionOption.BrokerageAccount,
                            UserUUID = positionOption.UserUUID
                        };  
                        _stockRepository.AddNewStock(thisStock);
                        //Add the Position to the Position table
                        var thisPosition = new Position
                        {
                            Name = positionOption.Name,
                            StockSymbol = positionOption.StockSymbol,
                            TotalPremium = positionOption.TotalPremium,
                            BrokerageAccount = positionOption.BrokerageAccount,
                            UserUUID = positionOption.UserUUID
                        };
                        _context.Positions.Add(thisPosition);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            if (positionOpenOptions != null)
            {
                foreach (var positionOption in positionOpenOptions)
                {
                    if (StockPositionExists(positionOption.StockSymbol))
                    {
                        var thisPosition = new Position
                        {
                            Name = positionOption.Name,
                            StockSymbol = positionOption.StockSymbol,
                            UnsettledPremium = positionOption.TotalPremium,
                            BrokerageAccount = positionOption.BrokerageAccount,
                            UserUUID = positionOption.UserUUID
                        };
                        _context.Positions.Add(thisPosition);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        //Add the Stock to the Stock table
                        var thisStock = new Stock
                        {
                            StockSymbol = positionOption.StockSymbol,
                            CompanyName = positionOption.Name,
                            BrokerageAccount = positionOption.BrokerageAccount,
                            UserUUID = positionOption.UserUUID
                        };
                        _stockRepository.AddNewStock(thisStock);
                        //Add the Position to the Position table
                        var thisPosition = new Position
                        {
                            Name = positionOption.Name,
                            StockSymbol = positionOption.StockSymbol,
                            UnsettledPremium = positionOption.TotalPremium,
                            BrokerageAccount = positionOption.BrokerageAccount,
                            UserUUID = positionOption.UserUUID
                        };
                        _context.Positions.Add(thisPosition);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            return CreatedAtAction("GetPosition", new { id = position.Id }, position);
        }

        // DELETE: api/Positions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(int id)
        {
            if (_context.Positions == null)
            {
                return NotFound();
            }
            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PositionExists(int id)
        {
            return (_context.Positions?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool StockPositionExists(string symbol)
        {
            var result = (_context.Stocks?.Any(e => e.StockSymbol == symbol)).GetValueOrDefault();
            return result;
        }

        private async Task<List<PositionDTO>> GetClosedPositionOptions()
        {
            var query = from ot in _context.PositionOptions.Where(x => x.isSettled == true)
                        group ot by new
                        {
                            ot.StockSymbol,
                            //ot.ExpirationDate,
                            //ot.OptionType,
                            //ot.StrikePrice,
                            ot.BrokerageAccount,
                            ot.UserUUID,
                            //ot.isSettled,
                        } into grouped
                        select new PositionDTO
                        {
                            StockSymbol = grouped.Key.StockSymbol,
                            Name = grouped.Key.StockSymbol,
                            isSettled = true, //grouped.Key.isSettled,
                            //StrikePrice = grouped.Key.StrikePrice,
                            //ExpirationDate = grouped.Key.ExpirationDate,
                            //OptionSymbol = grouped.Key.OptionSymbol,
                            BrokerageAccount = grouped.Key.BrokerageAccount,
                            UserUUID = grouped.Key.UserUUID,
                            //StockId = StockID(grouped.Key.StockSymbol, grouped.Key.UserUUID).Id,
                            TransactionCount = grouped.Count(),
                            //PositionTypeId = grouped.Sum(g => g.PositionTypeId),
                            TotalPremium = grouped.Sum(g => g.Amount),
                        };

            return await query
                .OrderBy(result => result.StockSymbol)
                //.ThenBy(result => result.OptionSymbol)
                .ToListAsync();
        }

        private async Task<List<PositionDTO>> GetOpenPositionOptions()
        {
            var query = from ot in _context.PositionOptions.Where(x => x.isSettled == false)    
                        group ot by new
                        {
                            ot.StockSymbol,
                            //ot.isSettled,
                            //ot.ExpirationDate,
                            //ot.OptionType,
                            //ot.StrikePrice,
                            ot.BrokerageAccount,
                            ot.UserUUID
                        } into grouped
                        select new PositionDTO
                        {
                            StockSymbol = grouped.Key.StockSymbol,
                            Name = grouped.Key.StockSymbol,
                            isSettled = false, // grouped.Key.isSettled,
                            //StrikePrice = grouped.Key.StrikePrice,
                            //ExpirationDate = grouped.Key.ExpirationDate,
                            //OptionSymbol = grouped.Key.OptionSymbol,
                            BrokerageAccount = grouped.Key.BrokerageAccount,
                            UserUUID = grouped.Key.UserUUID,
                            //StockId = StockID(grouped.Key.StockSymbol, grouped.Key.UserUUID).Id,
                            TransactionCount = grouped.Count(),
                            //PositionTypeId = grouped.Sum(g => g.PositionTypeId),
                            TotalPremium = grouped.Sum(g => g.Amount),
                        };

            return await query
                .OrderBy(result => result.StockSymbol)
                //.ThenBy(result => result.OptionSymbol)
                .ToListAsync();
        }
    }

    

}
