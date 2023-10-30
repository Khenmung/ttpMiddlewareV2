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
    public class ClassEvaluationOptionsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ClassEvaluationOptionsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ClassEvaluationOptions
        [HttpGet]
        public IQueryable<ClassEvaluationOption> GetClassEvaluationOptions()
        {
            return _context.ClassEvaluationOptions.AsQueryable().AsNoTracking(); ;
        }

        // GET: api/ClassEvaluationOptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassEvaluationOption>> GetClassEvaluationOption(int id)
        {
            var classEvaluationOption = await _context.ClassEvaluationOptions.FindAsync(id);

            if (classEvaluationOption == null)
            {
                return NotFound();
            }

            return classEvaluationOption;
        }

        // PUT: api/ClassEvaluationOptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassEvaluationOption(int id, ClassEvaluationOption classEvaluationOption)
        {
            if (id != classEvaluationOption.ClassEvaluationAnswerOptionsId)
            {
                return BadRequest();
            }

            _context.Entry(classEvaluationOption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassEvaluationOptionExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<ClassEvaluationOption> classEvaluationOption)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ClassEvaluationOptions.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            classEvaluationOption.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassEvaluationOptionExists(key))
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
        // POST: api/ClassEvaluationOptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClassEvaluationOption>> PostClassEvaluationOption([FromBody]ClassEvaluationOption classEvaluationOption)
        {
            _context.ClassEvaluationOptions.Add(classEvaluationOption);
            await _context.SaveChangesAsync();

            return Ok(classEvaluationOption);
        }

        // DELETE: api/ClassEvaluationOptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassEvaluationOption(int id)
        {
            var classEvaluationOption = await _context.ClassEvaluationOptions.FindAsync(id);
            if (classEvaluationOption == null)
            {
                return NotFound();
            }

            _context.ClassEvaluationOptions.Remove(classEvaluationOption);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassEvaluationOptionExists(int id)
        {
            return _context.ClassEvaluationOptions.Any(e => e.ClassEvaluationAnswerOptionsId == id);
        }
    }
}
