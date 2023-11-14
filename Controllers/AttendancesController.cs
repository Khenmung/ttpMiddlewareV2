using System;
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
    public class AttendancesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public AttendancesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/Attendances
        [HttpGet]
        public IQueryable<Attendance> GetAttendances()
        {
            try
            {
                var result= _context.Attendances.AsQueryable().AsNoTracking();
                return result;
            }
            catch(Exception ex) {
                return (IQueryable<Attendance>)BadRequest(ex.Message);
            }            
        }

        // GET: api/Attendances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> GetAttendance(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);

            if (attendance == null)
            {
                return NotFound();
            }

            return attendance;
        }

        // PUT: api/Attendances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendance(int id, Attendance attendance)
        {
            if (id != attendance.AttendanceId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(attendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<Attendance> attendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.Attendances.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            attendance.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(key))
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
        // POST: api/Attendances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Attendance>> PostAttendance([FromBody] Attendance attendance)
        {

            var _existing = await _context.Attendances.Where(x =>
            x.ClassSubjectId == attendance.ClassSubjectId 
            && x.StudentClassId == attendance.StudentClassId 
            && ((DateTime)x.AttendanceDate).Date== ((DateTime)attendance.AttendanceDate).Date
            && x.SubOrgId == attendance.SubOrgId
            && x.OrgId == attendance.OrgId
            && x.Deleted ==false
            ).ToListAsync();
            if (_existing.Count > 0)
            {
                return BadRequest("Record already exists!");
            }
            else
            {
                _context.Attendances.Add(attendance);
                await _context.SaveChangesAsync();
                return Ok(attendance);
            }
        }

        // DELETE: api/Attendances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.AttendanceId == id);
        }
    }
}
