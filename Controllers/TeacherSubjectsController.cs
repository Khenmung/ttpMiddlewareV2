using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]

    public class TeacherSubjectsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public TeacherSubjectsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/TeacherSubjects
        [HttpGet]
        public IQueryable<TeacherSubject> GetTeacherSubjects()
        {
            return _context.TeacherSubjects.AsQueryable().AsNoTracking();
        }

        // GET: api/TeacherSubjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherSubject>> GetTeacherSubject(int id)
        {
            var teacherSubject = await _context.TeacherSubjects.FindAsync(id);

            if (teacherSubject == null)
            {
                return NotFound();
            }

            return teacherSubject;
        }

        // PUT: api/TeacherSubjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacherSubject(int id, TeacherSubject teacherSubject)
        {
            if (id != teacherSubject.TeacherSubjectId)
            {
                return BadRequest();
            }

            _context.Entry(teacherSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherSubjectExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<TeacherSubject> teacherSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.TeacherSubjects.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            teacherSubject.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherSubjectExists(key))
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
        // POST: api/TeacherSubjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TeacherSubject>> PostTeacherSubject([FromBody]TeacherSubject teacherSubject)
        {
            _context.TeacherSubjects.Add(teacherSubject);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TeacherSubjectExists(teacherSubject.TeacherSubjectId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(teacherSubject);
        }

        // DELETE: api/TeacherSubjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacherSubject(int id)
        {
            var teacherSubject = await _context.TeacherSubjects.FindAsync(id);
            if (teacherSubject == null)
            {
                return NotFound();
            }

            _context.TeacherSubjects.Remove(teacherSubject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeacherSubjectExists(int id)
        {
            return _context.TeacherSubjects.Any(e => e.TeacherSubjectId == id);
        }
    }
}
