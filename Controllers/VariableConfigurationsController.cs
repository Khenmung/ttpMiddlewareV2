using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("VariableConfigurations")]
    [EnableQuery]
    public class VariableConfigurationsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public VariableConfigurationsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/VariableConfigurations
        [HttpGet]
        public IQueryable<VariableConfiguration> Get()
        {
            return _context.VariableConfigurations.AsQueryable().AsNoTracking();
        }

        // GET: api/VariableConfigurations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VariableConfiguration>> GetVariableConfiguration(short id)
        {
            var variableConfiguration = await _context.VariableConfigurations.FindAsync(id);

            if (variableConfiguration == null)
            {
                return NotFound();
            }

            return variableConfiguration;
        }

        // PUT: api/VariableConfigurations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(short id, VariableConfiguration variableConfiguration)
        {
            if (id != variableConfiguration.VariableConfigurationId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(variableConfiguration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VariableConfigurationExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<VariableConfiguration> variableConfiguration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.VariableConfigurations.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            variableConfiguration.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VariableConfigurationExists(key))
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
        // POST: api/VariableConfigurations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VariableConfiguration>> PostVariableConfiguration([FromBody]VariableConfiguration variableConfiguration)
        {
            _context.VariableConfigurations.Add(variableConfiguration);
            await _context.SaveChangesAsync();

            return Ok(variableConfiguration);
        }

        // DELETE: api/VariableConfigurations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVariableConfiguration(short id)
        {
            var variableConfiguration = await _context.VariableConfigurations.FindAsync(id);
            if (variableConfiguration == null)
            {
                return NotFound();
            }

            _context.VariableConfigurations.Remove(variableConfiguration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VariableConfigurationExists(short id)
        {
            return _context.VariableConfigurations.Any(e => e.VariableConfigurationId == id);
        }
    }
}
