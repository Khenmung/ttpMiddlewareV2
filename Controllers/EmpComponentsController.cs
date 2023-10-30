using System;
using System.Collections.Generic;
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
    public class EmpComponentsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public EmpComponentsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/EmpComponents
        [HttpGet]
        public IQueryable<EmpComponent> GetEmpComponents()
        {
            return _context.EmpComponents.AsQueryable().AsNoTracking();
        }

        // GET: api/EmpComponents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpComponent>> GetEmpComponent(short id)
        {
            var empComponent = await _context.EmpComponents.FindAsync(id);

            if (empComponent == null)
            {
                return NotFound();
            }

            return empComponent;
        }

        // PUT: api/EmpComponents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpComponent(short id, EmpComponent empComponent)
        {
            if (id != empComponent.EmpSalaryComponentId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(empComponent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpComponentExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<EmpComponent> empComponent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.EmpComponents.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            empComponent.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpComponentExists(key))
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
        // POST: api/EmpComponents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmpComponent>> PostEmpComponent([FromBody]EmpComponent empComponent)
        {
            _context.EmpComponents.Add(empComponent);
            await _context.SaveChangesAsync();

            return Ok(empComponent);
        }

        // DELETE: api/EmpComponents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpComponent(short id)
        {
            var empComponent = await _context.EmpComponents.FindAsync(id);
            if (empComponent == null)
            {
                return NotFound();
            }

            _context.EmpComponents.Remove(empComponent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpComponentExists(int id)
        {
            return _context.EmpComponents.Any(e => e.EmpSalaryComponentId == id);
        }
    }
}
