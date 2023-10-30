using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;

using ttpMiddleware.CommonFunctions;
using System;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery(MaxExpansionDepth = 3)]
    public class StudentClassSubjectsController : ODataController// ProtectedController
    {
        private readonly ttpauthContext _context;

        public StudentClassSubjectsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/StudentClassSubjects
        [HttpGet]
        public IQueryable<StudentClassSubject> GetStudentClassSubjects()
        {
            try
            {
                return _context.StudentClassSubjects.AsQueryable().AsNoTracking();
            }
            catch (Exception ex)
            {
                return (IQueryable<StudentClassSubject>)BadRequest(ex);
            }

        }

        // GET: api/StudentClassSubjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentClassSubject>> GetStudentClassSubject(int id)
        {
            var studentClassSubject = await _context.StudentClassSubjects.FindAsync(id);

            if (studentClassSubject == null)
            {
                return NotFound();
            }

            return studentClassSubject;
        }

        // PUT: api/StudentClassSubjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentClassSubject(int id, StudentClassSubject studentClassSubject)
        {
            if (id != studentClassSubject.StudentClassSubjectId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(studentClassSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentClassSubjectExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<StudentClassSubject> studentClassSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.StudentClassSubjects.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            studentClassSubject.Patch(entity);
            try
            {
                var subjectExist = _context.StudentClassSubjects.Where(x => 
                x.StudentClassId == entity.StudentClassId
                && x.StudentClassSubjectId != entity.StudentClassSubjectId
                && x.ClassSubjectId == entity.ClassSubjectId
                && x.SectionId == entity.SectionId
                && x.SemesterId == entity.SemesterId
                && x.BatchId == entity.BatchId
                && x.OrgId == entity.OrgId
                && x.SubOrgId == entity.SubOrgId).Select(s => s.StudentClassSubjectId).ToList();
                if (subjectExist.Any())
                {
                    return BadRequest("Record already exists.");
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentClassSubjectExists(key))
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
        // POST: api/StudentClassSubjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentClassSubject>> PostStudentClassSubject([FromBody] StudentClassSubject studentClassSubject)
        {
            var subjectExist = _context.StudentClassSubjects.Where(x => x.StudentClassId == studentClassSubject.StudentClassId
            && x.ClassSubjectId == studentClassSubject.ClassSubjectId
            && x.BatchId == studentClassSubject.BatchId
            && x.OrgId == studentClassSubject.OrgId
            && x.SubOrgId == studentClassSubject.SubOrgId).Select(s => s.StudentClassSubjectId).ToList();
            if (subjectExist.Any())
            {
                return BadRequest("Record already exists.");
            }
            _context.StudentClassSubjects.Add(studentClassSubject);
            await _context.SaveChangesAsync();

            return Ok(studentClassSubject);
        }

        // DELETE: api/StudentClassSubjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentClassSubject(int id)
        {
            var studentClassSubject = await _context.StudentClassSubjects.FindAsync(id);
            if (studentClassSubject == null)
            {
                return NotFound();
            }

            _context.StudentClassSubjects.Remove(studentClassSubject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentClassSubjectExists(int id)
        {
            return _context.StudentClassSubjects.Any(e => e.StudentClassSubjectId == id);
        }
    }
}
