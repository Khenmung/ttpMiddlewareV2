using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.Models;
using ttpMiddleware.CommonFunctions;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class LedgerPostingsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public LedgerPostingsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/LedgerPostings
        [HttpGet]
        public IQueryable<LedgerPosting> GetLedgerPostings()
        {
            return _context.LedgerPostings.AsQueryable().AsNoTracking();
        }

        // GET: api/LedgerPostings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LedgerPosting>> GetLedgerPosting(int id)
        {
            var ledgerPosting = await _context.LedgerPostings.FindAsync(id);

            if (ledgerPosting == null)
            {
                return NotFound();
            }

            return ledgerPosting;
        }

        // PUT: api/LedgerPostings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLedgerPosting(int id, LedgerPosting ledgerPosting)
        {
            if (id != ledgerPosting.LedgerPostingId)
            {
                return BadRequest();
            }

            _context.Entry(ledgerPosting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LedgerPostingExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<LedgerPosting> ledgerPosting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.LedgerPostings.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            ledgerPosting.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LedgerPostingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(entity);
        }
        // POST: api/LedgerPostings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LedgerPosting>> PostLedgerPosting([FromBody]LedgerPosting ledgerPosting)
        {
            _context.LedgerPostings.Add(ledgerPosting);
            await _context.SaveChangesAsync();

            return Ok(ledgerPosting);
        }

        // DELETE: api/LedgerPostings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLedgerPosting(int id)
        {
            var ledgerPosting = await _context.LedgerPostings.FindAsync(id);
            if (ledgerPosting == null)
            {
                return NotFound();
            }

            _context.LedgerPostings.Remove(ledgerPosting);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LedgerPostingExists(int id)
        {
            return _context.LedgerPostings.Any(e => e.LedgerPostingId == id);
        }
    }
}
