using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;
using ttpMiddleware.Models;using Microsoft.AspNet.OData;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("CustomerPlanFeatures")]
    [EnableQuery]
    public class CustomerPlanFeaturesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public CustomerPlanFeaturesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ApplicationFeatures
        [HttpGet]
        public IQueryable<CustomerPlanFeature> Get()
        {
            return _context.CustomerPlanFeatures.AsQueryable().AsNoTracking();
        }

        // GET: api/ApplicationFeatures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerPlanFeature>> Get(short id)
        {
            var customerPlanFeature = await _context.CustomerPlanFeatures.FindAsync(id);

            if (customerPlanFeature == null)
            {
                return NotFound();
            }

            return customerPlanFeature;
        }

        // PUT: api/ApplicationFeatures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationFeature(short id, ApplicationFeature applicationFeature)
        {
            if (id != applicationFeature.ApplicationFeatureId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(applicationFeature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerPlanFeatureExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<CustomerPlanFeature> customerPlanFeature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.CustomerPlanFeatures.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            customerPlanFeature.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerPlanFeatureExists(key))
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
        // POST: api/ApplicationFeatures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerPlanFeature>> PostCustomerPlanFeature([FromBody]CustomerPlanFeature customerPlanFeature)
        {
            _context.CustomerPlanFeatures.Add(customerPlanFeature);
            await _context.SaveChangesAsync();

            return Ok(customerPlanFeature);
        }

        // DELETE: api/ApplicationFeatures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationFeature(short id)
        {
            var applicationFeature = await _context.CustomerPlanFeatures.FindAsync(id);
            if (applicationFeature == null)
            {
                return NotFound();
            }

            _context.CustomerPlanFeatures.Remove(applicationFeature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerPlanFeatureExists(short id)
        {
            return _context.CustomerPlanFeatures.Any(e => e.CustomerPlanFeatureId == id);
        }
    }
}
