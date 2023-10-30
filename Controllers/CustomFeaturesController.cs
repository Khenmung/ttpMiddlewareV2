using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.CommonFunctions;
using ttpMiddleware.Models;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class CustomFeaturesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public CustomFeaturesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/CustomFeatures
        [HttpGet]
        public IQueryable<CustomFeature> GetCustomFeatures()
        {
            return _context.CustomFeatures.AsQueryable().AsNoTracking();
        }

        // GET: api/CustomFeatures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomFeature>> GetCustomFeature(int id)
        {
            var customFeature = await _context.CustomFeatures.FindAsync(id);

            if (customFeature == null)
            {
                return NotFound();
            }

            return customFeature;
        }

        // PUT: api/CustomFeatures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomFeature(int id, CustomFeature customFeature)
        {
            if (id != customFeature.CustomFeatureId)
            {
                return BadRequest();
            }

            _context.Entry(customFeature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomFeatureExists(id))
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
        
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<CustomFeature> customFeature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //_context.Entry(customFeature).State = EntityState.Modified;
            var entity = await _context.CustomFeatures.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            customFeature.Patch(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CustomFeatureExists(key))
                {
                    return BadRequest("Key not found");
                }
                else
                {
                    return BadRequest(ex);
                }
            }

            return Updated(entity);
        }
        // POST: api/CustomFeatures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomFeature>> PostCustomFeature(CustomFeature customFeature)
        {
            _context.CustomFeatures.Add(customFeature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomFeature", new { id = customFeature.CustomFeatureId }, customFeature);
        }

        // DELETE: api/CustomFeatures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomFeature(int id)
        {
            var customFeature = await _context.CustomFeatures.FindAsync(id);
            if (customFeature == null)
            {
                return NotFound();
            }

            _context.CustomFeatures.Remove(customFeature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomFeatureExists(int id)
        {
            return _context.CustomFeatures.Any(e => e.CustomFeatureId == id);
        }
    }
}
