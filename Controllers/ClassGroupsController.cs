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
    public class ClassGroupsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ClassGroupsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ClassGroups
        [HttpGet]
        public IQueryable<ClassGroup> GetClassGroups()
        {
            return _context.ClassGroups.AsQueryable().AsNoTracking();
        }

        // GET: api/ClassGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassGroup>> GetClassGroup(short id)
        {
            var classGroup = await _context.ClassGroups.FindAsync(id);

            if (classGroup == null)
            {
                return NotFound();
            }

            return classGroup;
        }

        // PUT: api/ClassGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassGroup(short id, ClassGroup classGroup)
        {
            if (id != classGroup.ClassGroupId)
            {
                return BadRequest();
            }

            _context.Entry(classGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassGroupExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<ClassGroup> classGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ClassGroups.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            classGroup.Patch(entity);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!ClassGroupExists(key))
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
        // POST: api/ClassGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClassGroup>> PostClassGroup([FromBody] ClassGroup classGroup)
        {
            try
            {
                _context.ClassGroups.Add(classGroup);
                await _context.SaveChangesAsync();

                return Ok(classGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/ClassGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassGroup(short id)
        {
            var classGroup = await _context.ClassGroups.FindAsync(id);
            if (classGroup == null)
            {
                return NotFound();
            }

            _context.ClassGroups.Remove(classGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassGroupExists(short id)
        {
            return _context.ClassGroups.Any(e => e.ClassGroupId == id);
        }
    }
}
