using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.CommonFunctions;
using ttpMiddleware.Models;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery(MaxExpansionDepth = 3)]
    public class EmployeeAttendancesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public EmployeeAttendancesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeAttendances
        [HttpGet]
        public IQueryable<EmployeeAttendance> GetEmployeeAttendances()
        {
            try
            {
                return _context.EmployeeAttendances.AsQueryable().AsNoTracking();
            }
            catch (Exception ex)
            {
                return (IQueryable<EmployeeAttendance>)BadRequest(ex);
            }
        }

        // GET: api/EmployeeAttendances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeAttendance>> GetEmployeeAttendance(int id)
        {
            var employeeAttendance = await _context.EmployeeAttendances.FindAsync(id);

            if (employeeAttendance == null)
            {
                return NotFound();
            }

            return employeeAttendance;
        }

        // PUT: api/EmployeeAttendances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeAttendance(int id, EmployeeAttendance employeeAttendance)
        {
            if (id != employeeAttendance.EmployeeAttendanceId)
            {
                return BadRequest();
            }

            _context.Entry(employeeAttendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeAttendanceExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<EmployeeAttendance> employeeAttendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.EmployeeAttendances.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            employeeAttendance.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeAttendanceExists(key))
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
        // POST: api/EmployeeAttendances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeAttendance>> PostEmployeeAttendance([FromBody] EmployeeAttendance employeeAttendance)
        {
            _context.EmployeeAttendances.Add(employeeAttendance);
            await _context.SaveChangesAsync();

            return Ok(employeeAttendance);
        }

        // DELETE: api/EmployeeAttendances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAttendance(int id)
        {
            var employeeAttendance = await _context.EmployeeAttendances.FindAsync(id);
            if (employeeAttendance == null)
            {
                return NotFound();
            }

            _context.EmployeeAttendances.Remove(employeeAttendance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeAttendanceExists(int id)
        {
            return _context.EmployeeAttendances.Any(e => e.EmployeeAttendanceId == id);
        }
    }
}
