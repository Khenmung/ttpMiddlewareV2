using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNet.OData.Routing;
using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("EmployeeMonthlySalaries")]
    [EnableQuery]
    public class EmployeeMonthlySalariesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public EmployeeMonthlySalariesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeMonthlySalaries
        [HttpGet]
        public IQueryable<EmployeeMonthlySalary> GetEmployeeMonthlySalaries()
        {
            return _context.EmployeeMonthlySalaries.AsQueryable().AsNoTracking();
        }

        // GET: api/EmployeeMonthlySalaries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeMonthlySalary>> GetEmployeeMonthlySalary(short id)
        {
            var employeeMonthlySalary = await _context.EmployeeMonthlySalaries.FindAsync(id);

            if (employeeMonthlySalary == null)
            {
                return NotFound();
            }

            return employeeMonthlySalary;
        }

        // PUT: api/EmployeeMonthlySalaries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeMonthlySalary(short id, EmployeeMonthlySalary employeeMonthlySalary)
        {
            if (id != employeeMonthlySalary.EmployeeMonthlySalaryId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(employeeMonthlySalary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeMonthlySalaryExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<EmployeeMonthlySalary> employeeMonthlySalary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.EmployeeMonthlySalaries.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            employeeMonthlySalary.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeMonthlySalaryExists(key))
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
        // POST: api/EmployeeMonthlySalaries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeMonthlySalary>> PostEmployeeMonthlySalary([FromBody]EmployeeMonthlySalary employeeMonthlySalary)
        {
            _context.EmployeeMonthlySalaries.Add(employeeMonthlySalary);
            await _context.SaveChangesAsync();

            return Ok(employeeMonthlySalary);
        }

        // DELETE: api/EmployeeMonthlySalaries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeMonthlySalary(short id)
        {
            var employeeMonthlySalary = await _context.EmployeeMonthlySalaries.FindAsync(id);
            if (employeeMonthlySalary == null)
            {
                return NotFound();
            }

            _context.EmployeeMonthlySalaries.Remove(employeeMonthlySalary);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeMonthlySalaryExists(short id)
        {
            return _context.EmployeeMonthlySalaries.Any(e => e.EmployeeMonthlySalaryId == id);
        }
    }
}
