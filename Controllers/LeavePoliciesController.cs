using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;
using ttpMiddleware.CommonFunctions;
using System;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class LeavePoliciesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public LeavePoliciesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/LeavePolicies
        [HttpGet]
        public IQueryable<LeavePolicy> GetLeavePolicies()
        {
            return _context.LeavePolicies.AsQueryable().AsNoTracking();
        }

        // GET: api/LeavePolicies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeavePolicy>> GetLeavePolicy(int id)
        {
            var leavePolicy = await _context.LeavePolicies.FindAsync(id);

            if (leavePolicy == null)
            {
                return NotFound();
            }

            return leavePolicy;
        }

        // PUT: api/LeavePolicies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeavePolicy(int id, LeavePolicy leavePolicy)
        {
            if (id != leavePolicy.LeavePolicyId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(leavePolicy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeavePolicyExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<LeavePolicy> leavePolicy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.LeavePolicies.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            leavePolicy.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeavePolicyExists(key))
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
        // POST: api/LeavePolicies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LeavePolicy>> PostLeavePolicy([FromBody]LeavePolicy leavePolicy)
        {
            if(!ModelState.IsValid)
            { return BadRequest(ModelState); }
            try
            {
                _context.LeavePolicies.Add(leavePolicy);
                await _context.SaveChangesAsync();

                return Ok(leavePolicy);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/LeavePolicies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeavePolicy(int id)
        {
            var leavePolicy = await _context.LeavePolicies.FindAsync(id);
            if (leavePolicy == null)
            {
                return NotFound();
            }

            _context.LeavePolicies.Remove(leavePolicy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeavePolicyExists(int id)
        {
            return _context.LeavePolicies.Any(e => e.LeavePolicyId == id);
        }
    }
}
