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

    public class QuestionBanksController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public QuestionBanksController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/QuestionBanks
        [HttpGet]
        public IQueryable<QuestionBank> GetQuestionBanks()
        {
            return _context.QuestionBanks.AsQueryable().AsNoTracking();
        }

        // GET: api/QuestionBanks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionBank>> GetQuestionBank(int id)
        {
            var questionBank = await _context.QuestionBanks.FindAsync(id);

            if (questionBank == null)
            {
                return NotFound();
            }

            return questionBank;
        }

        // PUT: api/QuestionBanks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionBank(int id, QuestionBank questionBank)
        {
            if (id != questionBank.QuestionBankId)
            {
                return BadRequest();
            }

            _context.Entry(questionBank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionBankExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<QuestionBank> questionBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.QuestionBanks.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            questionBank.Patch(entity);
            //var tran = _context.Database.BeginTransaction();
            try
            {
                await _context.SaveChangesAsync();
                
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //tran.Rollback();
                if (!QuestionBankExists(key))
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
        // POST: api/QuestionBanks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionBank>> PostQuestionBank([FromBody] QuestionBank questionBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.QuestionBanks.Add(questionBank);
                await _context.SaveChangesAsync();
                return Ok(questionBank);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }            
        }

        // DELETE: api/QuestionBanks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionBank(int id)
        {
            var questionBank = await _context.QuestionBanks.FindAsync(id);
            if (questionBank == null)
            {
                return NotFound();
            }

            _context.QuestionBanks.Remove(questionBank);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionBankExists(int id)
        {
            return _context.QuestionBanks.Any(e => e.QuestionBankId == id);
        }
    }
}
