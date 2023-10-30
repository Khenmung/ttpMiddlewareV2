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

using ttpMiddleware.CommonFunctions;
namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class PlanFeaturesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public PlanFeaturesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/PlanFeatures
        [HttpGet]
        public IQueryable<PlanFeature> GetPlanFeatures()
        {
            return _context.PlanFeatures.AsQueryable().AsNoTracking();
        }

        // GET: api/PlanFeatures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlanFeature>> GetPlanFeature(short id)
        {
            var planFeature = await _context.PlanFeatures.FindAsync(id);

            if (planFeature == null)
            {
                return NotFound();
            }

            return planFeature;
        }

        // PUT: api/PlanFeatures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlanFeature(short id, PlanFeature planFeature)
        {
            if (id != planFeature.PlanFeatureId)
            {
                return BadRequest();
            }

            _context.Entry(planFeature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanFeatureExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<PlanFeature> planFeature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.PlanFeatures.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            planFeature.Patch(entity);
            try
            {

                var perm = await _context.ApplicationFeatureRolesPerms.Where(x => x.PlanFeatureId == entity.PlanFeatureId
                && x.PlanId == entity.PlanId).FirstOrDefaultAsync();
                if (perm != null)
                {
                    perm.Active = entity.Active;
                    _context.Update(perm);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanFeatureExists(key))
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
        // POST: api/PlanFeatures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlanFeature>> PostPlanFeature([FromBody] PlanFeature planFeature)
        {
            _context.PlanFeatures.Add(planFeature);
            await _context.SaveChangesAsync();

            return Ok(planFeature);
        }

        // DELETE: api/PlanFeatures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlanFeature(short id)
        {
            var planFeature = await _context.PlanFeatures.FindAsync(id);
            if (planFeature == null)
            {
                return NotFound();
            }

            _context.PlanFeatures.Remove(planFeature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlanFeatureExists(short id)
        {
            return _context.PlanFeatures.Any(e => e.PlanFeatureId == id);
        }
    }
}
