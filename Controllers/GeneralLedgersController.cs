using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.Models;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class GeneralLedgersController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public GeneralLedgersController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/GeneralLedgers
        [HttpGet]
        public IQueryable<GeneralLedger> GetGeneralLedgers()
        {
            return _context.GeneralLedgers.AsQueryable().AsNoTracking();
        }

        // GET: api/GeneralLedgers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralLedger>> GetGeneralLedger(int id)
        {
            var generalLedger = await _context.GeneralLedgers.FindAsync(id);

            if (generalLedger == null)
            {
                return NotFound();
            }

            return generalLedger;
        }

        // PUT: api/GeneralLedgers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGeneralLedger(int id, GeneralLedger generalLedger)
        {
            if (id != generalLedger.GeneralLedgerId)
            {
                return BadRequest();
            }

            _context.Entry(generalLedger).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneralLedgerExists(id))
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

        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<GeneralLedger> generalLedger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.GeneralLedgers.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            generalLedger.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneralLedgerExists(key))
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
        // POST: api/GeneralLedgers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GeneralLedger>> PostGeneralLedger([FromBody] GeneralLedger generalLedger)
        {
            try
            {
                _context.GeneralLedgers.Add(generalLedger);
                await _context.SaveChangesAsync();

                return Ok(generalLedger);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
           
        }

        // DELETE: api/GeneralLedgers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeneralLedger(int id)
        {
            var generalLedger = await _context.GeneralLedgers.FindAsync(id);
            if (generalLedger == null)
            {
                return NotFound();
            }

            _context.GeneralLedgers.Remove(generalLedger);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GeneralLedgerExists(int id)
        {
            return _context.GeneralLedgers.Any(e => e.GeneralLedgerId == id);
        }
    }
}
