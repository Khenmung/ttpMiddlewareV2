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
    [EnableQuery]
    public class ExamStudentSubjectResultsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ExamStudentSubjectResultsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ExamStudentSubjectResults
        [HttpGet]
        public IQueryable<ExamStudentSubjectResult> GetExamStudentSubjectResults()
        {
            //try
            //{
            return _context.ExamStudentSubjectResults.AsQueryable().AsNoTracking();
            //}
            //catch(Exception ex)
            //{
            //    return BadRequest(ex);
            //}

        }

        // GET: api/ExamStudentSubjectResults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamStudentSubjectResult>> GetExamStudentSubjectResult(int id)
        {
            var examStudentSubjectResult = await _context.ExamStudentSubjectResults.FindAsync(id);

            if (examStudentSubjectResult == null)
            {
                return NotFound();
            }

            return examStudentSubjectResult;
        }

        // PUT: api/ExamStudentSubjectResults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamStudentSubjectResult(int id, ExamStudentSubjectResult examStudentSubjectResult)
        {
            if (id != examStudentSubjectResult.ExamStudentSubjectResultId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(examStudentSubjectResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamStudentSubjectResultExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<ExamStudentSubjectResult> examStudentSubjectResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ExamStudentSubjectResults.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            examStudentSubjectResult.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamStudentSubjectResultExists(key))
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
        // POST: api/ExamStudentSubjectResults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExamStudentSubjectResult>> PostExamStudentSubjectResult([FromBody] ExamStudentSubjectResult examStudentSubjectResult)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var duplicate = await _context.ExamStudentSubjectResults.Where(x =>
                x.OrgId == examStudentSubjectResult.OrgId
                && x.SubOrgId == examStudentSubjectResult.SubOrgId
                && x.ExamId == examStudentSubjectResult.ExamId
                && x.StudentClassId == examStudentSubjectResult.StudentClassId
                && x.StudentClassSubjectId == examStudentSubjectResult.StudentClassSubjectId                
                && x.ClassSubjectMarkComponentId == examStudentSubjectResult.ClassSubjectMarkComponentId
                && x.Deleted ==false).Select(s => s.ExamStudentSubjectResultId).ToListAsync();

                if (duplicate.Count > 0)
                {
                    return BadRequest("Duplicate data.");
                }
                _context.ExamStudentSubjectResults.Add(examStudentSubjectResult);
                await _context.SaveChangesAsync();

                return Ok(examStudentSubjectResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/ExamStudentSubjectResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamStudentSubjectResult(int id)
        {
            var examStudentSubjectResult = await _context.ExamStudentSubjectResults.FindAsync(id);
            if (examStudentSubjectResult == null)
            {
                return NotFound();
            }

            _context.ExamStudentSubjectResults.Remove(examStudentSubjectResult);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExamStudentSubjectResultExists(int id)
        {
            return _context.ExamStudentSubjectResults.Any(e => e.ExamStudentSubjectResultId == id);
        }
    }
}
