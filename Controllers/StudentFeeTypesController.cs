using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.Models;
using ttpMiddleware.CommonFunctions;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class StudentFeeTypesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public StudentFeeTypesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/StudentFeeTypes
        [HttpGet]
        public IQueryable<StudentFeeType> GetStudentFeeTypes()
        {
            return _context.StudentFeeTypes.AsQueryable().AsNoTracking();
        }

        // GET: api/StudentFeeTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentFeeType>> GetStudentFeeType(int id)
        {
            var studentFeeType = await _context.StudentFeeTypes.FindAsync(id);

            if (studentFeeType == null)
            {
                return NotFound();
            }

            return studentFeeType;
        }

        // PUT: api/StudentFeeTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentFeeType(int id, StudentFeeType studentFeeType)
        {
            if (id != studentFeeType.StudentFeeTypeId)
            {
                return BadRequest();
            }

            _context.Entry(studentFeeType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentFeeTypeExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<StudentFeeType> studentFeeType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.StudentFeeTypes.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            studentFeeType.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentFeeTypeExists(key))
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
        // POST: api/StudentFeeTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentFeeType>> PostStudentFeeType([FromBody]StudentFeeType studentFeeType)
        {
            _context.StudentFeeTypes.Add(studentFeeType);
            await _context.SaveChangesAsync();

            return Ok(studentFeeType);
        }

        // DELETE: api/StudentFeeTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentFeeType(int id)
        {
            var studentFeeType = await _context.StudentFeeTypes.FindAsync(id);
            if (studentFeeType == null)
            {
                return NotFound();
            }

            _context.StudentFeeTypes.Remove(studentFeeType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentFeeTypeExists(int id)
        {
            return _context.StudentFeeTypes.Any(e => e.StudentFeeTypeId == id);
        }
    }
}
