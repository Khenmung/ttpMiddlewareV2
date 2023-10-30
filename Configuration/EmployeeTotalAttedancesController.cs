//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNet.OData;
//using Microsoft.AspNet.OData.Routing;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ttpMiddleware.CommonFunctions;
//using ttpMiddleware.Models;

//namespace ttpMiddleware.Controllers
//{
//    [ODataRoutePrefix("[controller]")]
//    [EnableQuery]

//    public class EmployeeTotalAttedancesController : ProtectedController
//    {
//        private readonly ttpauthContext _context;

//        public EmployeeTotalAttedancesController(ttpauthContext context)
//        {
//            _context = context;
//        }

//        // GET: api/EmployeeTotalAttedances
//        //[HttpGet]
//        //public IQueryable<EmployeeTotalAttedance> GetEmployeeTotalAttedances()
//        //{
//        //    return _context.EmployeeTotalAttedances.AsQueryable().AsNoTracking();
//        //}

//        // GET: api/EmployeeTotalAttedances/5
//        //[HttpGet("{id}")]
//        //public async Task<ActionResult<EmployeeTotalAttedance>> GetEmployeeTotalAttedance(int id)
//        //{
//        //    var employeeTotalAttedance = await _context.EmployeeTotalAttedances.FindAsync(id);

//        //    if (employeeTotalAttedance == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    return employeeTotalAttedance;
//        //}

//        // PUT: api/EmployeeTotalAttedances/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutEmployeeTotalAttedance(int id, EmployeeTotalAttedance employeeTotalAttedance)
//        {
//            if (id != employeeTotalAttedance.EmployeeTotalAttendanceId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(employeeTotalAttedance).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!EmployeeTotalAttedanceExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }
//        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<EmployeeTotalAttedance> employeeTotalAttedance)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            var entity = await _context.EmployeeTotalAttedances.FindAsync(key);
//            if (entity == null)
//            {
//                return NotFound();
//            }
//            employeeTotalAttedance.Patch(entity);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException ex)
//            {
//                if (!EmployeeTotalAttedanceExists(key))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    return BadRequest(ex);
//                }
//            }

//            return Updated(entity);
//        }
//        // POST: api/EmployeeTotalAttedances
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<EmployeeTotalAttedance>> PostEmployeeTotalAttedance([FromBody] EmployeeTotalAttedance employeeTotalAttedance)
//        {
//            _context.EmployeeTotalAttedances.Add(employeeTotalAttedance);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException ex)
//            {
//                if (EmployeeTotalAttedanceExists(employeeTotalAttedance.EmployeeTotalAttendanceId))
//                {
//                    return Conflict();
//                }
//                else
//                {
//                    return BadRequest(ex);
//                }
//            }

//            return Ok(employeeTotalAttedance);
//        }

//        // DELETE: api/EmployeeTotalAttedances/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteEmployeeTotalAttedance(int id)
//        {
//            var employeeTotalAttedance = await _context.EmployeeTotalAttedances.FindAsync(id);
//            if (employeeTotalAttedance == null)
//            {
//                return NotFound();
//            }

//            _context.EmployeeTotalAttedances.Remove(employeeTotalAttedance);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool EmployeeTotalAttedanceExists(int id)
//        {
//            return _context.EmployeeTotalAttedances.Any(e => e.EmployeeTotalAttendanceId == id);
//        }
//    }
//}
