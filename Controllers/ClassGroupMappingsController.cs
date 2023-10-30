using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.Models;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class ClassGroupMappingsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ClassGroupMappingsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ClassGroupMappings
        [HttpGet]
        public IQueryable<ClassGroupMapping> GetClassGroupMappings()
        {
            try
            {
                return _context.ClassGroupMappings.AsQueryable().AsNoTracking();
            }
            catch(Exception ex)
            {
                return (IQueryable<ClassGroupMapping>)BadRequest(ex);
            }
            
        }

        // GET: api/ClassGroupMappings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassGroupMapping>> GetClassGroupMapping(short id)
        {
            var classGroupMapping = await _context.ClassGroupMappings.FindAsync(id);

            if (classGroupMapping == null)
            {
                return NotFound();
            }

            return classGroupMapping;
        }

        // PUT: api/ClassGroupMappings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassGroupMapping(short id, ClassGroupMapping classGroupMapping)
        {
            if (id != classGroupMapping.ClassGroupMappingId)
            {
                return BadRequest();
            }

            _context.Entry(classGroupMapping).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassGroupMappingExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<ClassGroupMapping> classGroupMapping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ClassGroupMappings.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            classGroupMapping.Patch(entity);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!ClassGroupMappingExists(key))
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
        // POST: api/ClassGroupMappings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClassGroupMapping>> PostClassGroupMapping([FromBody]ClassGroupMapping classGroupMapping)
        {
            _context.ClassGroupMappings.Add(classGroupMapping);
            await _context.SaveChangesAsync();

            return Ok(classGroupMapping);
        }

        // DELETE: api/ClassGroupMappings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassGroupMapping(short id)
        {
            var classGroupMapping = await _context.ClassGroupMappings.FindAsync(id);
            if (classGroupMapping == null)
            {
                return NotFound();
            }

            _context.ClassGroupMappings.Remove(classGroupMapping);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassGroupMappingExists(short id)
        {
            return _context.ClassGroupMappings.Any(e => e.ClassGroupMappingId == id);
        }
    }
}
