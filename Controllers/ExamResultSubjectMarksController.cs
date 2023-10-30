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
    public class ExamResultSubjectMarksController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ExamResultSubjectMarksController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ExamResultSubjectMarks
        [HttpGet]
        public IQueryable<ExamResultSubjectMark> GetExamResultSubjectMarks()
        {
            return _context.ExamResultSubjectMarks.AsQueryable().AsNoTracking();
        }

        // GET: api/ExamResultSubjectMarks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamResultSubjectMark>> GetExamResultSubjectMark(int id)
        {
            var examResultSubjectMark = await _context.ExamResultSubjectMarks.FindAsync(id);

            if (examResultSubjectMark == null)
            {
                return NotFound();
            }

            return examResultSubjectMark;
        }

        // PUT: api/ExamResultSubjectMarks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamResultSubjectMark(int id, ExamResultSubjectMark examResultSubjectMark)
        {
            if (id != examResultSubjectMark.ExamResultSubjectMarkId)
            {
                return BadRequest();
            }

            _context.Entry(examResultSubjectMark).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamResultSubjectMarkExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<ExamResultSubjectMark> examResultSubjectMark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ExamResultSubjectMarks.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            examResultSubjectMark.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ExamResultSubjectMarkExists(key))
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
        // POST: api/ExamResultSubjectMarks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExamResultSubjectMark>> PostExamResultSubjectMark([FromBody] ExamResultSubjectMark examResultSubjectMark)
        {
            try
            {
                _context.ExamResultSubjectMarks.Add(examResultSubjectMark);
                await _context.SaveChangesAsync();

                return Ok(examResultSubjectMark);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        // DELETE: api/ExamResultSubjectMarks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamResultSubjectMark(int id)
        {
            var examResultSubjectMark = await _context.ExamResultSubjectMarks.FindAsync(id);
            if (examResultSubjectMark == null)
            {
                return NotFound();
            }

            _context.ExamResultSubjectMarks.Remove(examResultSubjectMark);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExamResultSubjectMarkExists(int id)
        {
            return _context.ExamResultSubjectMarks.Any(e => e.ExamResultSubjectMarkId == id);
        }
    }
}
