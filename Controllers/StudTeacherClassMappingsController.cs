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
    public class StudTeacherClassMappingsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public StudTeacherClassMappingsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/StudTeacherClassMappings
        [HttpGet]
        public IQueryable<StudTeacherClassMapping> GetStudTeacherClassMappings()
        {
            return _context.StudTeacherClassMappings.AsQueryable().AsNoTracking();
        }

        // GET: api/StudTeacherClassMappings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudTeacherClassMapping>> GetStudTeacherClassMapping(int id)
        {
            var studTeacherClassMapping = await _context.StudTeacherClassMappings.FindAsync(id);

            if (studTeacherClassMapping == null)
            {
                return NotFound();
            }

            return studTeacherClassMapping;
        }
        //[HttpPatch]
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<StudTeacherClassMapping> studTeacherClassMapping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.StudTeacherClassMappings.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            studTeacherClassMapping.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudTeacherClassMappingExists(key))
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
        // PUT: api/StudTeacherClassMappings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPatch("{key}")]
        //public async Task<IActionResult> PatchStudTeacherClassMapping([FromODataUri] int key, [FromBody] StudTeacherClassMapping studTeacherClassMapping)
        //{
        //    if (key != studTeacherClassMapping.TeacherClassMappingId)
        //    {
        //        return (IActionResult)BadRequest();
        //    }

        //    _context.Entry(studTeacherClassMapping).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StudTeacherClassMappingExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/StudTeacherClassMappings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudTeacherClassMapping>> PostStudTeacherClassMapping([FromBody] StudTeacherClassMapping studTeacherClassMapping)
        {
            _context.StudTeacherClassMappings.Add(studTeacherClassMapping);
            await _context.SaveChangesAsync();

            return Ok(studTeacherClassMapping);
        }

        // DELETE: api/StudTeacherClassMappings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudTeacherClassMapping(int id)
        {
            var studTeacherClassMapping = await _context.StudTeacherClassMappings.FindAsync(id);
            if (studTeacherClassMapping == null)
            {
                return NotFound();
            }

            _context.StudTeacherClassMappings.Remove(studTeacherClassMapping);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudTeacherClassMappingExists(int id)
        {
            return _context.StudTeacherClassMappings.Any(e => e.TeacherClassMappingId == id);
        }
    }
}
