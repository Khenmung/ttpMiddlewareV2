using System;
using System.Collections.Generic;
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
    public class ClassFeesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ClassFeesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ClassFees
        [HttpGet]
        public IQueryable<ClassFee> GetClassFees()
        {
            return _context.ClassFees.AsQueryable().AsNoTracking();
        }

        // GET: api/ClassFees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassFee>> GetClassFee(short id)
        {
            var classFee = await _context.ClassFees.FindAsync(id);

            if (classFee == null)
            {
                return NotFound();
            }

            return classFee;
        }

        // PUT: api/ClassFees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassFee(short id, ClassFee classFee)
        {
            if (id != classFee.ClassFeeId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(classFee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassFeeExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<ClassFee> classFee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ClassFees.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            classFee.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassFeeExists(key))
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
        // POST: api/ClassFees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClassFee>> PostClassFee([FromBody]ClassFee classFee)
        {
            _context.ClassFees.Add(classFee);
            await _context.SaveChangesAsync();

            return Ok(classFee);
        }

        // DELETE: api/ClassFees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassFee(short id)
        {
            var classFee = await _context.ClassFees.FindAsync(id);
            if (classFee == null)
            {
                return NotFound();
            }

            _context.ClassFees.Remove(classFee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassFeeExists(short id)
        {
            return _context.ClassFees.Any(e => e.ClassFeeId == id);
        }
    }
}
