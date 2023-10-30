using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class TaskConfigurationsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public TaskConfigurationsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/TaskConfigurations
        [HttpGet]
        public IQueryable<TaskConfiguration> GetTaskConfigurations()
        {
            return _context.TaskConfigurations.AsQueryable().AsNoTracking();
        }

        // GET: api/TaskConfigurations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskConfiguration>> GetTaskConfiguration(int id)
        {
            var taskConfiguration = await _context.TaskConfigurations.FindAsync(id);

            if (taskConfiguration == null)
            {
                return NotFound();
            }

            return taskConfiguration;
        }

        // PUT: api/TaskConfigurations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskConfiguration(int id, TaskConfiguration taskConfiguration)
        {
            if (id != taskConfiguration.Id)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(taskConfiguration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskConfigurationExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<TaskConfiguration> taskConfiguration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.TaskConfigurations.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            taskConfiguration.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskConfigurationExists(key))
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
        // POST: api/TaskConfigurations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskConfiguration>> PostTaskConfiguration([FromBody]TaskConfiguration taskConfiguration)
        {
            _context.TaskConfigurations.Add(taskConfiguration);
            await _context.SaveChangesAsync();

            return Ok(taskConfiguration);
        }

        // DELETE: api/TaskConfigurations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskConfiguration(int id)
        {
            var taskConfiguration = await _context.TaskConfigurations.FindAsync(id);
            if (taskConfiguration == null)
            {
                return NotFound();
            }

            _context.TaskConfigurations.Remove(taskConfiguration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskConfigurationExists(int id)
        {
            return _context.TaskConfigurations.Any(e => e.Id == id);
        }
    }
}
