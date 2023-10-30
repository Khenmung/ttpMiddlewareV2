using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.CommonFunctions;
using ttpMiddleware.Models;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class StudentGradesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public StudentGradesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/StudentGrades
        [HttpGet]
        public IQueryable<StudentGrade> GetStudentGrades()
        {
            return _context.StudentGrades.AsQueryable().AsNoTracking() ;
        }

        // GET: api/StudentGrades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentGrade>> GetStudentGrade(short id)
        {
            var studentGrade = await _context.StudentGrades.FindAsync(id);

            if (studentGrade == null)
            {
                return NotFound();
            }

            return studentGrade;
        }
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<StudentGrade> studentGrade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.StudentGrades.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            studentGrade.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!StudentGradeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(ex);
                }
            }

            return Updated(entity);
        }
        // PUT: api/StudentGrades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentGrade(short id, StudentGrade studentGrade)
        {
            if (id != studentGrade.StudentGradeId)
            {
                return BadRequest();
            }

            _context.Entry(studentGrade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentGradeExists(id))
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

        // POST: api/StudentGrades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentGrade>> PostStudentGrade([FromBody] StudentGrade studentGrade)
        {
            _context.StudentGrades.Add(studentGrade);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (!StudentGradeExists(studentGrade.StudentGradeId))
                {
                    return Conflict();
                }
                else
                {
                    return BadRequest(ex);
                }
            }

            return Ok(studentGrade);
        }

        // DELETE: api/StudentGrades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentGrade(short id)
        {
            var studentGrade = await _context.StudentGrades.FindAsync(id);
            if (studentGrade == null)
            {
                return NotFound();
            }

            _context.StudentGrades.Remove(studentGrade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentGradeExists(short id)
        {
            return _context.StudentGrades.Any(e => e.StudentGradeId == id);
        }
    }
}
