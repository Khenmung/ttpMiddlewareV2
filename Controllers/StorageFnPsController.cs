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
    public class StorageFnPsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public StorageFnPsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/StorageFnPs
        [HttpGet]
        public IQueryable<StorageFnP> GetStorageFnPs()
        {
            try
            {
                return _context.StorageFnPs.AsQueryable().AsNoTracking();
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }

        // GET: api/StorageFnPs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StorageFnP>> GetStorageFnP(int id)
        {
            var storageFnP = await _context.StorageFnPs.FindAsync(id);

            if (storageFnP == null)
            {
                return NotFound();
            }

            return storageFnP;
        }

        // PUT: api/StorageFnPs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStorageFnP(int id, StorageFnP storageFnP)
        {
            if (id != storageFnP.FileId)
            {
                return BadRequest();
            }

            _context.Entry(storageFnP).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorageFnPExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<StorageFnP> storageFnP)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.StorageFnPs.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            storageFnP.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorageFnPExists(key))
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
        // POST: api/StorageFnPs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StorageFnP>> PostStorageFnP([FromBody]StorageFnP storageFnP)
        {
            _context.StorageFnPs.Add(storageFnP);
            await _context.SaveChangesAsync();

            return Ok(storageFnP);
        }

        // DELETE: api/StorageFnPs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStorageFnP(int id)
        {
            var storageFnP = await _context.StorageFnPs.FindAsync(id);
            if (storageFnP == null)
            {
                return NotFound();
            }

            _context.StorageFnPs.Remove(storageFnP);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StorageFnPExists(int id)
        {
            return _context.StorageFnPs.Any(e => e.FileId == id);
        }
    }
}
