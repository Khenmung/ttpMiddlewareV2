using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttpMiddleware.CommonFunctions;
using ttpMiddleware.Models;

namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    [EnableQuery]
    public class ExamNCalculatesController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ExamNCalculatesController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ExamNCalculates
        [HttpGet]
        public IQueryable<ExamNCalculate> GetExamNCalculates()
        {
            return _context.ExamNCalculates.AsQueryable().AsNoTracking();
        }

        // GET: api/ExamNCalculates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamNCalculate>> GetExamNCalculate(int id)
        {
            var examNCalculate = await _context.ExamNCalculates.FindAsync(id);

            if (examNCalculate == null)
            {
                return NotFound();
            }

            return examNCalculate;
        }

        // PUT: api/ExamNCalculates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamNCalculate(int id, ExamNCalculate examNCalculate)
        {
            if (id != examNCalculate.ExamNCalculateId)
            {
                return BadRequest();
            }

            _context.Entry(examNCalculate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamNCalculateExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<ExamNCalculate> examNCalculate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ExamNCalculates.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            examNCalculate.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ExamNCalculateExists(key))
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
        // POST: api/ExamNCalculates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExamNCalculate>> PostExamNCalculate([FromBody]ExamNCalculate examNCalculate)
        {
            _context.ExamNCalculates.Add(examNCalculate);
            await _context.SaveChangesAsync();

            return Ok(examNCalculate);
        }

        // DELETE: api/ExamNCalculates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamNCalculate(int id)
        {
            var examNCalculate = await _context.ExamNCalculates.FindAsync(id);
            if (examNCalculate == null)
            {
                return NotFound();
            }

            _context.ExamNCalculates.Remove(examNCalculate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExamNCalculateExists(int id)
        {
            return _context.ExamNCalculates.Any(e => e.ExamNCalculateId == id);
        }
    }
}
