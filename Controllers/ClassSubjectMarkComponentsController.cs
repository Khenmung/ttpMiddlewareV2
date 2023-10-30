using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class ClassSubjectMarkComponentsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ClassSubjectMarkComponentsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ClassSubjectMarkComponents
        [HttpGet]
        public IQueryable<ClassSubjectMarkComponent> GetClassSubjectMarkComponents()
        {
            return _context.ClassSubjectMarkComponents.AsQueryable().AsNoTracking();
        }

        // GET: api/ClassSubjectMarkComponents/5
        [HttpGet("{key}")]
        public async Task<ActionResult<ClassSubjectMarkComponent>> GetClassSubjectMarkComponent(short key)
        {
            var classSubjectMarkComponent = await _context.ClassSubjectMarkComponents.FindAsync(key);

            if (classSubjectMarkComponent == null)
            {
                return NotFound();
            }

            return classSubjectMarkComponent;
        }
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<ClassSubjectMarkComponent> classSubjectMarkComponent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ClassSubjectMarkComponents.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            classSubjectMarkComponent.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassSubjectMarkComponentExists(key))
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
        // PUT: api/ClassSubjectMarkComponents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassSubjectMarkComponent(short id, ClassSubjectMarkComponent classSubjectMarkComponent)
        {
            if (id != classSubjectMarkComponent.ClassSubjectMarkComponentId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(classSubjectMarkComponent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassSubjectMarkComponentExists(id))
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

        // POST: api/ClassSubjectMarkComponents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClassSubjectMarkComponent>> PostClassSubjectMarkComponent([FromBody] ClassSubjectMarkComponent classSubjectMarkComponent)
        {
            try
            {

                _context.ClassSubjectMarkComponents.Add(classSubjectMarkComponent);
                await _context.SaveChangesAsync();
                return Ok(classSubjectMarkComponent);
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

        // DELETE: api/ClassSubjectMarkComponents/5
        [HttpDelete("{key}")]
        public async Task<IActionResult> DeleteClassSubjectMarkComponent(short key)
        {
            var classSubjectMarkComponent = await _context.ClassSubjectMarkComponents.FindAsync(key);
            if (classSubjectMarkComponent == null)
            {
                return NotFound();
            }

            _context.ClassSubjectMarkComponents.Remove(classSubjectMarkComponent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassSubjectMarkComponentExists(short key)
        {
            return _context.ClassSubjectMarkComponents.Any(e => e.ClassSubjectMarkComponentId == key);
        }
    }
}
