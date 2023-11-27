using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.CommonFunctions;
using ttpMiddleware.Models;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class StudentStaturesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public StudentStaturesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/StudentStatures
        [HttpGet]
        public IQueryable<StudentStature> GetStudentStatures()
        {
            return _context.StudentStatures.AsQueryable().AsNoTracking();
        }

        // GET: api/StudentStatures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentStature>> GetStudentStature(int id)
        {
            var studentStature = await _context.StudentStatures.FindAsync(id);

            if (studentStature == null)
            {
                return NotFound();
            }

            return studentStature;
        }

        // PUT: api/StudentStatures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentStature(int id, StudentStature studentStature)
        {
            if (id != studentStature.StudentStatureId)
            {
                return BadRequest();
            }

            _context.Entry(studentStature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentStatureExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<StudentStature> studentStature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.StudentStatures.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            studentStature.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentStatureExists(key))
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
        // POST: api/StudentStatures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentStature>> PostStudentStature([FromBody]StudentStature studentStature)
        {
            _context.StudentStatures.Add(studentStature);
            await _context.SaveChangesAsync();

            return Ok(studentStature);
        }

        // DELETE: api/StudentStatures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentStature(int id)
        {
            var studentStature = await _context.StudentStatures.FindAsync(id);
            if (studentStature == null)
            {
                return NotFound();
            }

            _context.StudentStatures.Remove(studentStature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentStatureExists(int id)
        {
            return _context.StudentStatures.Any(e => e.StudentStatureId == id);
        }
    }
}
