using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;

using ttpMiddleware.CommonFunctions;
using System;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("ExamSlots")]
    [EnableQuery]
    public class ExamSlotsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ExamSlotsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ExamSlots
        [HttpGet]
        public IQueryable<ExamSlot> GetExamSlots()
        {
            try
            {
                return _context.ExamSlots.AsQueryable().AsNoTracking();
            }
            catch (Exception ex)
            {
                return (IQueryable<ExamSlot>)BadRequest(ex.Message);
            }
        }

        // GET: api/ExamSlots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamSlot>> GetExamSlot(short id)
        {
            var examSlot = await _context.ExamSlots.FindAsync(id);

            if (examSlot == null)
            {
                return NotFound();
            }

            return examSlot;
        }

        // PUT: api/ExamSlots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamSlot(short id, ExamSlot examSlot)
        {
            if (id != examSlot.ExamSlotId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(examSlot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamSlotExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<ExamSlot> examSlot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ExamSlots.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            examSlot.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamSlotExists(key))
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
        // POST: api/ExamSlots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExamSlot>> PostExamSlot([FromBody] ExamSlot examSlot)
        {
            _context.ExamSlots.Add(examSlot);
            await _context.SaveChangesAsync();

            return Ok(examSlot);
        }

        // DELETE: api/ExamSlots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamSlot(short id)
        {
            var examSlot = await _context.ExamSlots.FindAsync(id);
            if (examSlot == null)
            {
                return NotFound();
            }

            _context.ExamSlots.Remove(examSlot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExamSlotExists(short id)
        {
            return _context.ExamSlots.Any(e => e.ExamSlotId == id);
        }
    }
}
