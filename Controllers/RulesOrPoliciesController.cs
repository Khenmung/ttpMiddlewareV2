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

    public class RulesOrPoliciesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public RulesOrPoliciesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/RulesOrPolicies
        [HttpGet]
        public IQueryable<RulesOrPolicy> GetRulesOrPolicies()
        {
            return _context.RulesOrPolicies.AsQueryable().AsNoTracking();
        }

        // GET: api/RulesOrPolicies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RulesOrPolicy>> GetRulesOrPolicy(int id)
        {
            var rulesOrPolicy = await _context.RulesOrPolicies.FindAsync(id);

            if (rulesOrPolicy == null)
            {
                return NotFound();
            }

            return rulesOrPolicy;
        }

        // PUT: api/RulesOrPolicies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRulesOrPolicy(int id, RulesOrPolicy rulesOrPolicy)
        {
            if (id != rulesOrPolicy.RulesOrPolicyId)
            {
                return BadRequest();
            }

            _context.Entry(rulesOrPolicy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RulesOrPolicyExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<RulesOrPolicy> rulesOrPolicy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //_context.Entry(customFeature).State = EntityState.Modified;
            var entity = await _context.RulesOrPolicies.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            rulesOrPolicy.Patch(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!RulesOrPolicyExists(key))
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
        // POST: api/RulesOrPolicies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RulesOrPolicy>> PostRulesOrPolicy([FromBody]RulesOrPolicy rulesOrPolicy)
        {
            _context.RulesOrPolicies.Add(rulesOrPolicy);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (RulesOrPolicyExists(rulesOrPolicy.RulesOrPolicyId))
                {
                    return Conflict();
                }
                else
                {
                    return BadRequest(ex);
                }
            }

            return Ok(rulesOrPolicy);
        }

        // DELETE: api/RulesOrPolicies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRulesOrPolicy(int id)
        {
            var rulesOrPolicy = await _context.RulesOrPolicies.FindAsync(id);
            if (rulesOrPolicy == null)
            {
                return NotFound();
            }

            _context.RulesOrPolicies.Remove(rulesOrPolicy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RulesOrPolicyExists(int id)
        {
            return _context.RulesOrPolicies.Any(e => e.RulesOrPolicyId == id);
        }
    }
}
