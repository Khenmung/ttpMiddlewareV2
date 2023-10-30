//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNet.OData;
//using Microsoft.AspNet.OData.Routing;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ttpMiddleware.CommonFunctions;
//using ttpMiddleware.Models;

//namespace ttpMiddleware.Controllers
//{
//    [ODataRoutePrefix("[controller]")]
//    [EnableQuery]

//    public class EmployeeGroupActivityParticipantsController : ProtectedController
//    {
//        private readonly ttpauthContext _context;

//        public EmployeeGroupActivityParticipantsController(ttpauthContext context)
//        {
//            _context = context;
//        }

//        // GET: api/EmployeeGroupActivityParticipants
//        [HttpGet]
//        public IQueryable<EmployeeGroupActivityParticipant> GetEmployeeGroupActivityParticipants()
//        {
//            return _context.EmployeeGroupActivityParticipants.AsQueryable().AsNoTracking();
//        }

//        // GET: api/EmployeeGroupActivityParticipants/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<EmployeeGroupActivityParticipant>> GetEmployeeGroupActivityParticipant(int id)
//        {
//            var employeeGroupActivityParticipant = await _context.EmployeeGroupActivityParticipants.FindAsync(id);

//            if (employeeGroupActivityParticipant == null)
//            {
//                return NotFound();
//            }

//            return employeeGroupActivityParticipant;
//        }

//        // PUT: api/EmployeeGroupActivityParticipants/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutEmployeeGroupActivityParticipant(int id, EmployeeGroupActivityParticipant employeeGroupActivityParticipant)
//        {
//            if (id != employeeGroupActivityParticipant.GroupActivityParticipantId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(employeeGroupActivityParticipant).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!EmployeeGroupActivityParticipantExists(id))
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
//        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<EmployeeGroupActivityParticipant> employeeGroupActivityParticipant)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            var entity = await _context.EmployeeGroupActivityParticipants.FindAsync(key);
//            if (entity == null)
//            {
//                return NotFound();
//            }
//            employeeGroupActivityParticipant.Patch(entity);
//            try
//            {
//                await _context.SaveChangesAsync();
//                return Updated(entity);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex);
//            }
//        }

//        // POST: api/EmployeeGroupActivityParticipants
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<EmployeeGroupActivityParticipant>> PostEmployeeGroupActivityParticipant([FromBody]EmployeeGroupActivityParticipant employeeGroupActivityParticipant)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            try
//            {
//                _context.EmployeeGroupActivityParticipants.Add(employeeGroupActivityParticipant);
//                await _context.SaveChangesAsync();

//                return Ok(employeeGroupActivityParticipant);
//            }
//            catch(Exception ex)
//            {
//                return BadRequest(ex);
//            }
            
//        }

//        // DELETE: api/EmployeeGroupActivityParticipants/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteEmployeeGroupActivityParticipant(int id)
//        {
//            var employeeGroupActivityParticipant = await _context.EmployeeGroupActivityParticipants.FindAsync(id);
//            if (employeeGroupActivityParticipant == null)
//            {
//                return NotFound();
//            }

//            _context.EmployeeGroupActivityParticipants.Remove(employeeGroupActivityParticipant);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool EmployeeGroupActivityParticipantExists(int id)
//        {
//            return _context.EmployeeGroupActivityParticipants.Any(e => e.GroupActivityParticipantId == id);
//        }
//    }
//}
