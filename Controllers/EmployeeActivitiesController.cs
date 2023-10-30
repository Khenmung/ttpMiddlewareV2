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
    [EnableQuery]
    public class EmployeeActivitiesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public EmployeeActivitiesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeActivities
        [HttpGet]
        public IQueryable<EmployeeActivity> GetEmployeeActivities()
        {
            return _context.EmployeeActivities.AsQueryable().AsNoTracking();
        }

        // GET: api/EmployeeActivities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeActivity>> GetEmployeeActivity(int id)
        {
            var employeeActivity = await _context.EmployeeActivities.FindAsync(id);

            if (employeeActivity == null)
            {
                return NotFound();
            }

            return employeeActivity;
        }

        // PUT: api/EmployeeActivities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeActivity(int id, EmployeeActivity employeeActivity)
        {
            if (id != employeeActivity.EmployeeActivityId)
            {
                return BadRequest();
            }

            _context.Entry(employeeActivity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeActivityExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<EmployeeActivity> employeeActivity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.EmployeeActivities.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            employeeActivity.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
                return Updated(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST: api/EmployeeActivities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeActivity>> PostEmployeeActivity([FromBody] EmployeeActivity employeeActivity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.EmployeeActivities.Add(employeeActivity);
                await _context.SaveChangesAsync();

                return Ok(employeeActivity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        // DELETE: api/EmployeeActivities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeActivity(int id)
        {
            var employeeActivity = await _context.EmployeeActivities.FindAsync(id);
            if (employeeActivity == null)
            {
                return NotFound();
            }

            _context.EmployeeActivities.Remove(employeeActivity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeActivityExists(int id)
        {
            return _context.EmployeeActivities.Any(e => e.EmployeeActivityId == id);
        }
    }
}
