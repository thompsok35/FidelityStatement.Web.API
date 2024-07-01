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
    public class TransactionActionsController : ControllerBase
    {
        private readonly FidelityStatementDbContext _context;

        public TransactionActionsController(FidelityStatementDbContext context)
        {
            _context = context;
        }

        // GET: api/TransactionActions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionAction>>> GetTransactionActions()
        {
          if (_context.TransactionActions == null)
          {
              return NotFound();
          }
            return await _context.TransactionActions.ToListAsync();
        }

        // GET: api/TransactionActions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionAction>> GetTransactionAction(int id)
        {
          if (_context.TransactionActions == null)
          {
              return NotFound();
          }
            var transactionAction = await _context.TransactionActions.FindAsync(id);

            if (transactionAction == null)
            {
                return NotFound();
            }

            return transactionAction;
        }

        // PUT: api/TransactionActions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactionAction(int id, TransactionAction transactionAction)
        {
            if (id != transactionAction.Id)
            {
                return BadRequest();
            }

            _context.Entry(transactionAction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionActionExists(id))
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

        // POST: api/TransactionActions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TransactionAction>> PostTransactionAction(TransactionAction transactionAction)
        {
          if (_context.TransactionActions == null)
          {
              return Problem("Entity set 'FidelityStatementDbContext.TransactionActions'  is null.");
          }
            transactionAction.CreatedDate = DateTime.Now;
            _context.TransactionActions.Add(transactionAction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransactionAction", new { id = transactionAction.Id }, transactionAction);
        }

        // DELETE: api/TransactionActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionAction(int id)
        {
            if (_context.TransactionActions == null)
            {
                return NotFound();
            }
            var transactionAction = await _context.TransactionActions.FindAsync(id);
            if (transactionAction == null)
            {
                return NotFound();
            }

            _context.TransactionActions.Remove(transactionAction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionActionExists(int id)
        {
            return (_context.TransactionActions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
