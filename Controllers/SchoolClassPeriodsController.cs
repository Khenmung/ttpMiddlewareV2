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
    public class SchoolClassPeriodsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public SchoolClassPeriodsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/SchoolClassPeriods
        [HttpGet]
        public IQueryable<SchoolClassPeriod> GetSchoolClassPeriods()
        {
            return _context.SchoolClassPeriods.AsQueryable().AsNoTracking();
        }

        // GET: api/SchoolClassPeriods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolClassPeriod>> GetSchoolClassPeriod(int id)
        {
            var schoolClassPeriod = await _context.SchoolClassPeriods.FindAsync(id);

            if (schoolClassPeriod == null)
            {
                return NotFound();
            }

            return schoolClassPeriod;
        }

        // PUT: api/SchoolClassPeriods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchoolClassPeriod(int id, SchoolClassPeriod schoolClassPeriod)
        {
            if (id != schoolClassPeriod.SchoolClassPeriodId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(schoolClassPeriod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolClassPeriodExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<SchoolClassPeriod> schoolClassPeriod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.SchoolClassPeriods.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            schoolClassPeriod.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolClassPeriodExists(key))
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
        // POST: api/SchoolClassPeriods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SchoolClassPeriod>> PostSchoolClassPeriod([FromBody]SchoolClassPeriod schoolClassPeriod)
        {
            _context.SchoolClassPeriods.Add(schoolClassPeriod);
            await _context.SaveChangesAsync();

            return Ok(schoolClassPeriod);
        }

        // DELETE: api/SchoolClassPeriods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolClassPeriod(int id)
        {
            var schoolClassPeriod = await _context.SchoolClassPeriods.FindAsync(id);
            if (schoolClassPeriod == null)
            {
                return NotFound();
            }

            _context.SchoolClassPeriods.Remove(schoolClassPeriod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SchoolClassPeriodExists(int id)
        {
            return _context.SchoolClassPeriods.Any(e => e.SchoolClassPeriodId == id);
        }
    }
}
