using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class EmployeeEducationHistoriesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public EmployeeEducationHistoriesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeEducationHistories
        [HttpGet]
        public IQueryable<EmployeeEducationHistory> GetEmployeeEducationHistories()
        {
            return _context.EmployeeEducationHistories.AsQueryable().AsNoTracking();
        }

        // GET: api/EmployeeEducationHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeEducationHistory>> GetEmployeeEducationHistory(int id)
        {
            var employeeEducationHistory = await _context.EmployeeEducationHistories.FindAsync(id);

            if (employeeEducationHistory == null)
            {
                return NotFound();
            }

            return employeeEducationHistory;
        }

        // PUT: api/EmployeeEducationHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeEducationHistory(int id, EmployeeEducationHistory employeeEducationHistory)
        {
            if (id != employeeEducationHistory.EmployeeEducationHistoryId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(employeeEducationHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeEducationHistoryExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<EmployeeEducationHistory> employeeEducationHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.EmployeeEducationHistories.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            employeeEducationHistory.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeEducationHistoryExists(key))
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
        // POST: api/EmployeeEducationHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeEducationHistory>> PostEmployeeEducationHistory([FromBody]EmployeeEducationHistory employeeEducationHistory)
        {            
            _context.EmployeeEducationHistories.Add(employeeEducationHistory);
            await _context.SaveChangesAsync();

            return Ok(employeeEducationHistory);
        }

        // DELETE: api/EmployeeEducationHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeEducationHistory(int id)
        {
            var employeeEducationHistory = await _context.EmployeeEducationHistories.FindAsync(id);
            if (employeeEducationHistory == null)
            {
                return NotFound();
            }

            _context.EmployeeEducationHistories.Remove(employeeEducationHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeEducationHistoryExists(int id)
        {
            return _context.EmployeeEducationHistories.Any(e => e.EmployeeEducationHistoryId == id);
        }
    }
}
