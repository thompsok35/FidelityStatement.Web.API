using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FidelityStatement.Web.API.DAL;
using FidelityStatement.Web.API.DAL.Models;

namespace FidelityStatement.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionTypesController : ControllerBase
    {
        private readonly FidelityStatementDbContext _context;

        public PositionTypesController(FidelityStatementDbContext context)
        {
            _context = context;
        }

        // GET: api/PositionTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionType>>> GetPositionTypes()
        {
          if (_context.PositionTypes == null)
          {
              return NotFound();
          }
            return await _context.PositionTypes.ToListAsync();
        }

        // GET: api/PositionTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PositionType>> GetPositionType(int id)
        {
          if (_context.PositionTypes == null)
          {
              return NotFound();
          }
            var positionType = await _context.PositionTypes.FindAsync(id);

            if (positionType == null)
            {
                return NotFound();
            }

            return positionType;
        }

        // PUT: api/PositionTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPositionType(int id, PositionType positionType)
        {
            if (id != positionType.Id)
            {
                return BadRequest();
            }

            _context.Entry(positionType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionTypeExists(id))
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

        // POST: api/PositionTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PositionType>> PostPositionType(PositionType positionType)
        {
          if (_context.PositionTypes == null)
          {
              return Problem("Entity set 'FidelityStatementDbContext.PositionTypes'  is null.");
          }
            _context.PositionTypes.Add(positionType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPositionType", new { id = positionType.Id }, positionType);
        }

        // DELETE: api/PositionTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePositionType(int id)
        {
            if (_context.PositionTypes == null)
            {
                return NotFound();
            }
            var positionType = await _context.PositionTypes.FindAsync(id);
            if (positionType == null)
            {
                return NotFound();
            }

            _context.PositionTypes.Remove(positionType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PositionTypeExists(int id)
        {
            return (_context.PositionTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
