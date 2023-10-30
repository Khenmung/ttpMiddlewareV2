using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.Models;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]

    public class ClassEvaluationsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ClassEvaluationsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ClassEvaluations
        [HttpGet]
        public IQueryable<ClassEvaluation> GetClassEvaluations()
        {
            return _context.ClassEvaluations.AsQueryable().AsNoTracking();
        }

        // GET: api/ClassEvaluations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassEvaluation>> GetClassEvaluation(int id)
        {
            var ClassEvaluation = await _context.ClassEvaluations.FindAsync(id);

            if (ClassEvaluation == null)
            {
                return NotFound();
            }

            return ClassEvaluation;
        }

        // PUT: api/ClassEvaluations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassEvaluation(int id, ClassEvaluation ClassEvaluation)
        {
            if (id != ClassEvaluation.ClassEvaluationId)
            {
                return BadRequest();
            }

            _context.Entry(ClassEvaluation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassEvaluationExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<ClassEvaluation> ClassEvaluation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ClassEvaluations.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            
            ClassEvaluation.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            
            }
            catch (DbUpdateConcurrencyException)
            {
                //tran.Rollback();
                if (!ClassEvaluationExists(key))
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
        // POST: api/ClassEvaluations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClassEvaluation>> PostClassEvaluation([FromBody] ClassEvaluation ClassEvaluation)
        {
            _context.ClassEvaluations.Add(ClassEvaluation);
            await _context.SaveChangesAsync();

            return Ok(ClassEvaluation);
        }

        // DELETE: api/ClassEvaluations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassEvaluation(int id)
        {
            var ClassEvaluation = await _context.ClassEvaluations.FindAsync(id);
            if (ClassEvaluation == null)
            {
                return NotFound();
            }

            _context.ClassEvaluations.Remove(ClassEvaluation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassEvaluationExists(int id)
        {
            return _context.ClassEvaluations.Any(e => e.ClassEvaluationId == id);
        }
    }
}
