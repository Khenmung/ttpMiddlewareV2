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
    public class SlotAndClassSubjectsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public SlotAndClassSubjectsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/SlotAndClassSubjects
        [HttpGet]
        public IQueryable<SlotAndClassSubject> GetSlotAndClassSubjects()
        {
            return _context.SlotAndClassSubjects.AsQueryable().AsNoTracking();
        }

        // GET: api/SlotAndClassSubjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SlotAndClassSubject>> GetSlotAndClassSubject(int id)
        {
            var slotAndClassSubject = await _context.SlotAndClassSubjects.FindAsync(id);

            if (slotAndClassSubject == null)
            {
                return NotFound();
            }

            return slotAndClassSubject;
        }

        // PUT: api/SlotAndClassSubjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSlotAndClassSubject(int id, SlotAndClassSubject slotAndClassSubject)
        {
            if (id != slotAndClassSubject.SlotClassSubjectId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(slotAndClassSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlotAndClassSubjectExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<SlotAndClassSubject> slotAndClassSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.SlotAndClassSubjects.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            slotAndClassSubject.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlotAndClassSubjectExists(key))
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
        // POST: api/SlotAndClassSubjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SlotAndClassSubject>> PostSlotAndClassSubject([FromBody]SlotAndClassSubject slotAndClassSubject)
        {
            _context.SlotAndClassSubjects.Add(slotAndClassSubject);
            await _context.SaveChangesAsync();

            return Ok(slotAndClassSubject);
        }

        // DELETE: api/SlotAndClassSubjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlotAndClassSubject(int id)
        {
            var slotAndClassSubject = await _context.SlotAndClassSubjects.FindAsync(id);
            if (slotAndClassSubject == null)
            {
                return NotFound();
            }

            _context.SlotAndClassSubjects.Remove(slotAndClassSubject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SlotAndClassSubjectExists(int id)
        {
            return _context.SlotAndClassSubjects.Any(e => e.SlotClassSubjectId == id);
        }
    }
}
