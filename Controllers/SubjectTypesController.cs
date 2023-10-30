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
    public class SubjectTypesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public SubjectTypesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/SubjectTypes
        [HttpGet]
        public IQueryable<SubjectType> GetSubjectTypes()
        {
            return _context.SubjectTypes.AsQueryable().AsNoTracking();
        }

        // GET: api/SubjectTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectType>> GetSubjectType(short id)
        {
            var subjectType = await _context.SubjectTypes.FindAsync(id);

            if (subjectType == null)
            {
                return NotFound();
            }

            return subjectType;
        }

        // PUT: api/SubjectTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        public async Task<IActionResult> Patch([FromODataUri]short key,[FromBody] Delta<SubjectType> subjectType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.SubjectTypes.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            subjectType.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectTypeExists(key))
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

        // POST: api/SubjectTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubjectType>> PostSubjectType([FromBody] SubjectType subjectType)
        {
            try
            {
                _context.SubjectTypes.Add(subjectType);
                await _context.SaveChangesAsync();
                return Ok(subjectType);
            }
            catch (DbUpdateException ex)
            {
                throw;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // DELETE: api/SubjectTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubjectType(short id)
        {
            var subjectType = await _context.SubjectTypes.FindAsync(id);
            if (subjectType == null)
            {
                return NotFound();
            }

            _context.SubjectTypes.Remove(subjectType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubjectTypeExists(short id)
        {
            return _context.SubjectTypes.Any(e => e.SubjectTypeId == id);
        }
    }
}
