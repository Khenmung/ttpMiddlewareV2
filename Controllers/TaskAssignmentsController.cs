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
    public class TaskAssignmentsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public TaskAssignmentsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/TaskAssignments
        [HttpGet]
        public IQueryable<TaskAssignment> Get()
        {
            return _context.TaskAssignments.AsQueryable().AsNoTracking();
        }

        // GET: api/TaskAssignments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskAssignment>> GetTaskAssignment(int id)
        {
            var taskAssignment = await _context.TaskAssignments.FindAsync(id);

            if (taskAssignment == null)
            {
                return NotFound();
            }

            return taskAssignment;
        }

        // PUT: api/TaskAssignments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskAssignment(int id, TaskAssignment taskAssignment)
        {
            if (id != taskAssignment.AssignmentId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(taskAssignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskAssignmentExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<TaskAssignment> taskAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.TaskAssignments.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            taskAssignment.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskAssignmentExists(key))
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
        // POST: api/TaskAssignments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskAssignment>> PostTaskAssignment([FromBody]TaskAssignment taskAssignment)
        {
            _context.TaskAssignments.Add(taskAssignment);
            await _context.SaveChangesAsync();

            return Ok(taskAssignment);
        }

        // DELETE: api/TaskAssignments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskAssignment(int id)
        {
            var taskAssignment = await _context.TaskAssignments.FindAsync(id);
            if (taskAssignment == null)
            {
                return NotFound();
            }

            _context.TaskAssignments.Remove(taskAssignment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskAssignmentExists(int id)
        {
            return _context.TaskAssignments.Any(e => e.AssignmentId == id);
        }
    }
}
