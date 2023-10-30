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
    [EnableQuery(MaxExpansionDepth = 3)]

    public class QuestionBankNExamsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public QuestionBankNExamsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/QuestionBankNExams
        [HttpGet]
        public IQueryable<QuestionBankNExam> GetQuestionBankNExams()
        {
            return _context.QuestionBankNExams.AsQueryable().AsNoTracking();
        }

        // GET: api/QuestionBankNExams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionBankNExam>> GetQuestionBankNExam(int id)
        {
            var questionBankNExam = await _context.QuestionBankNExams.FindAsync(id);

            if (questionBankNExam == null)
            {
                return NotFound();
            }

            return questionBankNExam;
        }

        // PUT: api/QuestionBankNExams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionBankNExam(int id, QuestionBankNExam questionBankNExam)
        {
            if (id != questionBankNExam.QuestionBankNExamId)
            {
                return BadRequest();
            }

            _context.Entry(questionBankNExam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionBankNExamExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<QuestionBankNExam> questionBanknExam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.QuestionBankNExams.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            questionBanknExam.Patch(entity);
            //var tran = _context.Database.BeginTransaction();
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                //tran.Rollback();
                if (!QuestionBankNExamExists(key))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(ex);
                }
            }
            catch (Exception ex)
            {
                //tran.Rollback();
                return BadRequest(ex);
            }

            return Updated(entity);
        }
        // POST: api/QuestionBankNExams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionBankNExam>> PostQuestionBankNExam([FromBody] QuestionBankNExam questionBankNExam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.QuestionBankNExams.Add(questionBankNExam);
                await _context.SaveChangesAsync();

                return Ok(questionBankNExam);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/QuestionBankNExams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionBankNExam(int id)
        {
            var questionBankNExam = await _context.QuestionBankNExams.FindAsync(id);
            if (questionBankNExam == null)
            {
                return NotFound();
            }

            _context.QuestionBankNExams.Remove(questionBankNExam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionBankNExamExists(int id)
        {
            return _context.QuestionBankNExams.Any(e => e.QuestionBankNExamId == id);
        }
    }
}
