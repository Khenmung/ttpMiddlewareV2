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
    public class EmpEmployeeSkillsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public EmpEmployeeSkillsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/EmpEmployeeSkills
        [HttpGet]
        public IQueryable<EmpEmployeeSkill> GetEmpEmployeeSkills()
        {
            return _context.EmpEmployeeSkills.AsQueryable().AsNoTracking();
        }

        // GET: api/EmpEmployeeSkills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpEmployeeSkill>> GetEmpEmployeeSkill(int id)
        {
            var empEmployeeSkill = await _context.EmpEmployeeSkills.FindAsync(id);

            if (empEmployeeSkill == null)
            {
                return NotFound();
            }

            return empEmployeeSkill;
        }

        // PUT: api/EmpEmployeeSkills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpEmployeeSkill(int id, EmpEmployeeSkill empEmployeeSkill)
        {
            if (id != empEmployeeSkill.EmpEmployeeSkillId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(empEmployeeSkill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEmployeeSkillExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<EmpEmployeeSkill> empEmployeeSkill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.EmpEmployeeSkills.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            empEmployeeSkill.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpEmployeeSkillExists(key))
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
        // POST: api/EmpEmployeeSkills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmpEmployeeSkill>> PostEmpEmployeeSkill([FromBody]EmpEmployeeSkill empEmployeeSkill)
        {
            _context.EmpEmployeeSkills.Add(empEmployeeSkill);
            await _context.SaveChangesAsync();

            return Ok(empEmployeeSkill);
        }

        // DELETE: api/EmpEmployeeSkills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpEmployeeSkill(int id)
        {
            var empEmployeeSkill = await _context.EmpEmployeeSkills.FindAsync(id);
            if (empEmployeeSkill == null)
            {
                return NotFound();
            }

            _context.EmpEmployeeSkills.Remove(empEmployeeSkill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpEmployeeSkillExists(int id)
        {
            return _context.EmpEmployeeSkills.Any(e => e.EmpEmployeeSkillId == id);
        }
    }
}
