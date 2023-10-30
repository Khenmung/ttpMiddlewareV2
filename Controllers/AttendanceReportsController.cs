using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;


using ttpMiddleware.CommonFunctions;
namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class AttendanceReportsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public AttendanceReportsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/AttendanceReports
        [HttpGet]
        public IQueryable<AttendanceReport> GetAttendanceReports()
        {
            return _context.AttendanceReports.AsQueryable().AsNoTracking();
        }

        // GET: api/AttendanceReports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceReport>> GetAttendanceReport(int id)
        {
            var attendanceReport = await _context.AttendanceReports.FindAsync(id);

            if (attendanceReport == null)
            {
                return NotFound();
            }

            return attendanceReport;
        }

        // PUT: api/AttendanceReports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendanceReport(int id, AttendanceReport attendanceReport)
        {
            if (id != attendanceReport.AttendanceReportId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(attendanceReport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceReportExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<AttendanceReport> attendanceReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.AttendanceReports.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            attendanceReport.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceReportExists(key))
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
        // POST: api/AttendanceReports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AttendanceReport>> PostAttendanceReport(AttendanceReport attendanceReport)
        {
            _context.AttendanceReports.Add(attendanceReport);
            await _context.SaveChangesAsync();

            return Ok(attendanceReport);
        }

        // DELETE: api/AttendanceReports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendanceReport(int id)
        {
            var attendanceReport = await _context.AttendanceReports.FindAsync(id);
            if (attendanceReport == null)
            {
                return NotFound();
            }

            _context.AttendanceReports.Remove(attendanceReport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttendanceReportExists(int id)
        {
            return _context.AttendanceReports.Any(e => e.AttendanceReportId == id);
        }
    }
}
