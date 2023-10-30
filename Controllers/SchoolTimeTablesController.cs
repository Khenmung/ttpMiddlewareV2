using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;
using System;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class SchoolTimeTablesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public SchoolTimeTablesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/SchoolTimeTables
        [HttpGet]
        public IQueryable<SchoolTimeTable> GetSchoolTimeTables()
        {
            return _context.SchoolTimeTables.AsQueryable().AsNoTracking();
        }

        // GET: api/SchoolTimeTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolTimeTable>> GetSchoolTimeTable(int id)
        {
            var schoolTimeTable = await _context.SchoolTimeTables.FindAsync(id);

            if (schoolTimeTable == null)
            {
                return NotFound();
            }

            return schoolTimeTable;
        }

        // PUT: api/SchoolTimeTables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchoolTimeTable(int id, SchoolTimeTable schoolTimeTable)
        {
            if (id != schoolTimeTable.TimeTableId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(schoolTimeTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolTimeTableExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<SchoolTimeTable> schoolTimeTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.SchoolTimeTables.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            schoolTimeTable.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolTimeTableExists(key))
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
        // POST: api/SchoolTimeTables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SchoolTimeTable>> Post([FromBody]SchoolTimeTable schoolTimeTable)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Incomplete data");
                }
                else
                {
                    _context.SchoolTimeTables.Add(schoolTimeTable);
                    await _context.SaveChangesAsync();

                    return Ok(schoolTimeTable);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // DELETE: api/SchoolTimeTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolTimeTable(int id)
        {
            var schoolTimeTable = await _context.SchoolTimeTables.FindAsync(id);
            if (schoolTimeTable == null)
            {
                return NotFound();
            }

            _context.SchoolTimeTables.Remove(schoolTimeTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SchoolTimeTableExists(int id)
        {
            return _context.SchoolTimeTables.Any(e => e.TimeTableId == id);
        }
    }
}
