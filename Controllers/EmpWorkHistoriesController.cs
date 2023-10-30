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
    public class EmpWorkHistoriesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public EmpWorkHistoriesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/EmpWorkHistories
        [HttpGet]
        public IQueryable<EmpWorkHistory> GetEmpWorkHistories()
        {
            return _context.EmpWorkHistories.AsQueryable().AsNoTracking();
        }

        // GET: api/EmpWorkHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpWorkHistory>> GetEmpWorkHistory(int id)
        {
            var empWorkHistory = await _context.EmpWorkHistories.FindAsync(id);

            if (empWorkHistory == null)
            {
                return NotFound();
            }

            return empWorkHistory;
        }

        // PUT: api/EmpWorkHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpWorkHistory(int id, EmpWorkHistory empWorkHistory)
        {
            if (id != empWorkHistory.EmpWorkHistoryId)
            {
                return BadRequest();
            }

            _context.Entry(empWorkHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpWorkHistoryExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<EmpWorkHistory> empWorkHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.EmpWorkHistories.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            empWorkHistory.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpWorkHistoryExists(key))
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
        
        // POST: api/EmpWorkHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmpWorkHistory>> PostEmpWorkHistory([FromBody] EmpWorkHistory empWorkHistory)
        {
            _context.EmpWorkHistories.Add(empWorkHistory);
            await _context.SaveChangesAsync();

            return Ok(empWorkHistory);
        }

        // DELETE: api/EmpWorkHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpWorkHistory(int id)
        {
            var empWorkHistory = await _context.EmpWorkHistories.FindAsync(id);
            if (empWorkHistory == null)
            {
                return NotFound();
            }

            _context.EmpWorkHistories.Remove(empWorkHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpWorkHistoryExists(int id)
        {
            return _context.EmpWorkHistories.Any(e => e.EmpWorkHistoryId == id);
        }
    }
}
