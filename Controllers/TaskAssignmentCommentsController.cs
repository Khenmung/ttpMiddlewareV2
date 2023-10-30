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
    public class TaskAssignmentCommentsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public TaskAssignmentCommentsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/TaskAssignmentComments
        [HttpGet]
        public IQueryable<TaskAssignmentComment> GetTaskAssignmentComments()
        {
            return _context.TaskAssignmentComments.AsQueryable().AsNoTracking();
        }

        // GET: api/TaskAssignmentComments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskAssignmentComment>> GetTaskAssignmentComment(int id)
        {
            var taskAssignmentComment = await _context.TaskAssignmentComments.FindAsync(id);

            if (taskAssignmentComment == null)
            {
                return NotFound();
            }

            return taskAssignmentComment;
        }

        // PUT: api/TaskAssignmentComments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskAssignmentComment(int id, TaskAssignmentComment taskAssignmentComment)
        {
            if (id != taskAssignmentComment.AssignmentCommentId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(taskAssignmentComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskAssignmentCommentExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<TaskAssignmentComment> taskAssignmentComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.TaskAssignmentComments.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            taskAssignmentComment.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskAssignmentCommentExists(key))
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
        // POST: api/TaskAssignmentComments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskAssignmentComment>> PostTaskAssignmentComment([FromBody]TaskAssignmentComment taskAssignmentComment)
        {
            _context.TaskAssignmentComments.Add(taskAssignmentComment);
            await _context.SaveChangesAsync();

            return Ok(taskAssignmentComment);
        }

        // DELETE: api/TaskAssignmentComments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskAssignmentComment(int id)
        {
            var taskAssignmentComment = await _context.TaskAssignmentComments.FindAsync(id);
            if (taskAssignmentComment == null)
            {
                return NotFound();
            }

            _context.TaskAssignmentComments.Remove(taskAssignmentComment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskAssignmentCommentExists(int id)
        {
            return _context.TaskAssignmentComments.Any(e => e.AssignmentCommentId == id);
        }
    }
}
