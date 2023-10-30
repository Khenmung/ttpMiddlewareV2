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
    public class FeeDefinitionsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public FeeDefinitionsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/FeeDefinitions
        [HttpGet]
        public IQueryable<FeeDefinition> GetFeeDefinitions()
        {
            return _context.FeeDefinitions.AsQueryable().AsNoTracking();
        }

        // GET: api/FeeDefinitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeeDefinition>> GetFeeDefinition(short id)
        {
            var feeDefinition = await _context.FeeDefinitions.FindAsync(id);

            if (feeDefinition == null)
            {
                return NotFound();
            }

            return feeDefinition;
        }

        // PUT: api/FeeDefinitions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeeDefinition(short id, FeeDefinition feeDefinition)
        {
            if (id != feeDefinition.FeeDefinitionId)
            {
                return BadRequest();
            }

            _context.Entry(feeDefinition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeeDefinitionExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<FeeDefinition> feeDefinition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.FeeDefinitions.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            feeDefinition.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeeDefinitionExists(key))
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
        // POST: api/FeeDefinitions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FeeDefinition>> PostFeeDefinition([FromBody]FeeDefinition feeDefinition)
        {
            _context.FeeDefinitions.Add(feeDefinition);
            await _context.SaveChangesAsync();
            return Ok(feeDefinition);
        }

        // DELETE: api/FeeDefinitions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeeDefinition(short id)
        {
            var feeDefinition = await _context.FeeDefinitions.FindAsync(id);
            if (feeDefinition == null)
            {
                return NotFound();
            }

            _context.FeeDefinitions.Remove(feeDefinition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeeDefinitionExists(short id)
        {
            return _context.FeeDefinitions.Any(e => e.FeeDefinitionId == id);
        }
    }
}
