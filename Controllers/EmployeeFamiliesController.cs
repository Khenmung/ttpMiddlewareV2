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
    public class EmployeeFamiliesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public EmployeeFamiliesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeFamilies
        [HttpGet]
        public IQueryable<EmployeeFamily> GetEmployeeFamilies()
        {
            return _context.EmployeeFamilies.AsQueryable().AsNoTracking();
        }

        // GET: api/EmployeeFamilies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeFamily>> GetEmployeeFamily(int id)
        {
            var employeeFamily = await _context.EmployeeFamilies.FindAsync(id);

            if (employeeFamily == null)
            {
                return NotFound();
            }

            return employeeFamily;
        }

        // PUT: api/EmployeeFamilies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeFamily(int id, EmployeeFamily employeeFamily)
        {
            if (id != employeeFamily.EmployeeFamilyId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(employeeFamily).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeFamilyExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<EmployeeFamily> employeeFamily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.EmployeeFamilies.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            employeeFamily.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeFamilyExists(key))
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
        // POST: api/EmployeeFamilies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeFamily>> PostEmployeeFamily([FromBody]EmployeeFamily employeeFamily)
        {
            _context.EmployeeFamilies.Add(employeeFamily);
            await _context.SaveChangesAsync();

            return Ok(employeeFamily);
        }

        // DELETE: api/EmployeeFamilies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeFamily(int id)
        {
            var employeeFamily = await _context.EmployeeFamilies.FindAsync(id);
            if (employeeFamily == null)
            {
                return NotFound();
            }

            _context.EmployeeFamilies.Remove(employeeFamily);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeFamilyExists(int id)
        {
            return _context.EmployeeFamilies.Any(e => e.EmployeeFamilyId == id);
        }
    }
}
