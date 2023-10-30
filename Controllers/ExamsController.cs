using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Routing;
using System.Data;
using ttpMiddleware.CommonFunctions;
using System;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("Exams")]
    public class ExamsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ExamsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/Exams
        [HttpGet]
        [EnableQuery]
        public IQueryable<Exam> GetExams()
        {
            try
            {
                return _context.Exams.AsQueryable().AsNoTracking();
            }
            catch(Exception ex)
            {
                return (IQueryable<Exam>)BadRequest(ex);
            }
         
        }

        // GET: api/Exams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetExam(short id)
        {
            var exam = await _context.Exams.FindAsync(id);

            if (exam == null)
            {
                return NotFound();
            }

            return exam;
        }

        // PUT: api/Exams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExam(short id, Exam exam)
        {
            if (id != exam.ExamId)
            {
                return (IActionResult)BadRequest();
            }

            _context.Entry(exam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] short key, [FromBody] Delta<Exam> exam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.Exams.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            exam.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
                
                
               
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(key))
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
        // POST: api/Exams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Exam>> PostExam([FromBody]Exam exam)
        {
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();

            return Ok(exam);
        }

        // DELETE: api/Exams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(short id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool Evaluate(string expression)
        {
            //expression = expression.ToUpper().Replace("IIF(", "IF(").Replace("IF(", "IIF(");
            using (DataTable dt = new DataTable())
            {
                return (bool)dt.Compute(expression, null);
            }
        }
        private bool ExamExists(short id)
        {
            return _context.Exams.Any(e => e.ExamId == id);
        }
    }
}
