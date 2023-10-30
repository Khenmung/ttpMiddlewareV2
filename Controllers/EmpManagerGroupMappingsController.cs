using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("EmpManagerGroupMappings")]
    [EnableQuery]
    public class EmpManagerGroupMappingsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public EmpManagerGroupMappingsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/EmpManagerGroupMappings
        [HttpGet]
        public IQueryable<EmpManagerGroupMapping> GetEmpManagerGroupMappings()
        {
            return _context.EmpManagerGroupMappings.AsQueryable().AsNoTracking();
        }

        // GET: api/EmpManagerGroupMappings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpManagerGroupMapping>> GetEmpManagerGroupMapping(short id)
        {
            var empManagerGroupMapping = await _context.EmpManagerGroupMappings.FindAsync(id);

            if (empManagerGroupMapping == null)
            {
                return NotFound();
            }

            return empManagerGroupMapping;
        }

        // PUT: api/EmpManagerGroupMappings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpManagerGroupMapping(short id, EmpManagerGroupMapping empManagerGroupMapping)
        {
            if (id != empManagerGroupMapping.ManagerGroupMappingId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(empManagerGroupMapping).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpManagerGroupMappingExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<EmpManagerGroupMapping> empManagerGroupMapping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.EmpManagerGroupMappings.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            empManagerGroupMapping.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpManagerGroupMappingExists(key))
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
        // POST: api/EmpManagerGroupMappings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmpManagerGroupMapping>> PostEmpManagerGroupMapping([FromBody]EmpManagerGroupMapping empManagerGroupMapping)
        {
            _context.EmpManagerGroupMappings.Add(empManagerGroupMapping);
            await _context.SaveChangesAsync();

            return Ok(empManagerGroupMapping);
        }

        // DELETE: api/EmpManagerGroupMappings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpManagerGroupMapping(short id)
        {
            var empManagerGroupMapping = await _context.EmpManagerGroupMappings.FindAsync(id);
            if (empManagerGroupMapping == null)
            {
                return NotFound();
            }

            _context.EmpManagerGroupMappings.Remove(empManagerGroupMapping);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpManagerGroupMappingExists(short id)
        {
            return _context.EmpManagerGroupMappings.Any(e => e.ManagerGroupMappingId == id);
        }
    }
}
