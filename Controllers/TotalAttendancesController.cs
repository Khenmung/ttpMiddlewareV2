using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.CommonFunctions;
using ttpMiddleware.Models;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]

    public class TotalAttendancesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public TotalAttendancesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/TotalAttendances
        [HttpGet]
        public IQueryable<TotalAttendance> GetTotalAttendances()
        {
            try
            {
                return _context.TotalAttendances.AsQueryable().AsNoTracking();
            }
            catch(Exception ex)
            {
                return (IQueryable<TotalAttendance>)BadRequest(ex);
            }
            
        }

        // GET: api/TotalAttendances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TotalAttendance>> GetTotalAttendance(int id)
        {
            var totalAttendance = await _context.TotalAttendances.FindAsync(id);

            if (totalAttendance == null)
            {
                return NotFound();
            }

            return totalAttendance;
        }

        // PUT: api/TotalAttendances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTotalAttendance(int id, TotalAttendance totalAttendance)
        {
            if (id != totalAttendance.TotalAttendanceId)
            {
                return BadRequest();
            }

            _context.Entry(totalAttendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TotalAttendanceExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<TotalAttendance> totalAttendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.TotalAttendances.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            totalAttendance.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!TotalAttendanceExists(key))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(ex);
                }
            }

            return Updated(entity);
        }
        // POST: api/TotalAttendances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TotalAttendance>> PostTotalAttendance([FromBody]TotalAttendance totalAttendance)
        {
            _context.TotalAttendances.Add(totalAttendance);
            await _context.SaveChangesAsync();

            return Ok(totalAttendance);
        }

        // DELETE: api/TotalAttendances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTotalAttendance(int id)
        {
            var totalAttendance = await _context.TotalAttendances.FindAsync(id);
            if (totalAttendance == null)
            {
                return NotFound();
            }

            _context.TotalAttendances.Remove(totalAttendance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TotalAttendanceExists(int id)
        {
            return _context.TotalAttendances.Any(e => e.TotalAttendanceId == id);
        }
    }
}
