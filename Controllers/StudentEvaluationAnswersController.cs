using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ttpMiddleware.Models;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class StudentEvaluationAnwerController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public StudentEvaluationAnwerController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/StudentEvaluations
        [HttpGet]
        public IQueryable<StudentEvaluationAnswer> GetStudentEvaluationAnswers()
        {
            return _context.StudentEvaluationAnswers.AsQueryable().AsNoTracking();
        }

        // GET: api/StudentEvaluations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentEvaluationAnswer>> GetStudentEvaluationAnswer(int id)
        {
            var studentEvaluation = await _context.StudentEvaluationAnswers.FindAsync(id);

            if (studentEvaluation == null)
            {
                return NotFound();
            }

            return studentEvaluation;
        }

        // PUT: api/StudentEvaluations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentEvaluationAnswer(int id, StudentEvaluationAnswer studentEvaluationAnswer)
        {
            if (id != studentEvaluationAnswer.ClassEvaluationAnswerOptionsId)
            {
                return BadRequest();
            }

            _context.Entry(studentEvaluationAnswer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentEvaluationAnswerExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<StudentEvaluationAnswer> studentEvaluationAnswer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.StudentEvaluationAnswers.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            studentEvaluationAnswer.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                //tran.Rollback();
                if (!StudentEvaluationAnswerExists(key))
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
        // POST: api/StudentEvaluations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentEvaluationAnswer>> PostStudentEvaluationAnswer([FromBody] StudentEvaluationAnswer studentEvaluationAnswer)
        {
            _context.StudentEvaluationAnswers.Add(studentEvaluationAnswer);
            await _context.SaveChangesAsync();

            return Ok(studentEvaluationAnswer);
            //using var tran = _context.Database.BeginTransaction();
            //try
            //{
            //    JToken jsonValues = studentEvaluationAnswer;
            //    StudentEvaluationAnswer _studentEvaluationAnswer = null;

            //    foreach (var x in jsonValues)
            //    {
            //        _studentEvaluationAnswer = x.ToObject<StudentEvaluationAnswer>();
            //        if (_studentEvaluationAnswer.StudentEvaluationAnswerId > 0)
            //        {
            //            var existingstudent = await _context.StudentEvaluationAnswers.Where(x => x.StudentEvaluationAnswerId == _studentEvaluationAnswer.StudentEvaluationAnswerId).FirstOrDefaultAsync();

            //            foreach (PropertyInfo prop in existingstudent.GetType().GetProperties())
            //            {
            //                if (prop.GetValue(_studentEvaluationAnswer, null) != null)
            //                    prop.SetValue(existingstudent, prop.GetValue(_studentEvaluationAnswer, null));
            //            }

            //            _context.StudentEvaluationAnswers.Update(existingstudent);
            //            _context.SaveChanges();

            //        }
            //        else
            //        {
            //            var existingstudent = await _context.StudentEvaluationAnswers.Where(x => x.s == _studentEvaluationAnswer.StudentClassId && x.ClassEvaluationId == _studentEvaluationAnswer.ClassEvaluationId).ToListAsync();
            //            if (existingstudent.Count() == 0)
            //            {
            //                _context.StudentEvaluationAnswers.Add(_studentEvaluationAnswer);
            //                await _context.SaveChangesAsync();
            //            }
            //        }
            //    }
            //    tran.Commit();

            //    return Ok(_studentEvaluationAnswer);

            //}
            //catch (Exception ex)
            //{
            //    tran.Rollback();
            //    throw;
            //}
        }

        // DELETE: api/StudentEvaluations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentEvaluationAnswers(int id)
        {
            var StudentEvaluationAnswers = await _context.StudentEvaluationAnswers.FindAsync(id);
            if (StudentEvaluationAnswers == null)
            {
                return NotFound();
            }

            _context.StudentEvaluationAnswers.Remove(StudentEvaluationAnswers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentEvaluationAnswerExists(int id)
        {
            return _context.StudentEvaluationAnswers.Any(e => e.StudentEvaluationAnswerId == id);
        }
    }
}
