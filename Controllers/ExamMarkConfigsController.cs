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
    public class ExamMarkConfigsController : ProtectedController
    {
        private readonly ttpauthContext _context;

        public ExamMarkConfigsController(ttpauthContext context)
        {
            _context = context;
        }

        // GET: api/ExamMarkConfigs
        [HttpGet]
        public IQueryable<ExamMarkConfig> GetExamMarkConfigs()
        {
            return _context.ExamMarkConfigs.AsQueryable().AsNoTracking();
        }

        // GET: api/ExamMarkConfigs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamMarkConfig>> GetExamMarkConfig(int id)
        {
            var examMarkConfig = await _context.ExamMarkConfigs.FindAsync(id);

            if (examMarkConfig == null)
            {
                return NotFound();
            }

            return examMarkConfig;
        }

        // PUT: api/ExamMarkConfigs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamMarkConfig(int id, ExamMarkConfig examMarkConfig)
        {
            if (id != examMarkConfig.ExamMarkConfigId)
            {
                return BadRequest();
            }

            _context.Entry(examMarkConfig).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamMarkConfigExists(id))
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
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<ExamMarkConfig> examMarkConfig)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ExamMarkConfigs.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            examMarkConfig.Patch(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ExamMarkConfigExists(key))
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
        // POST: api/ExamMarkConfigs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExamMarkConfig>> PostExamMarkConfig([FromBody]ExamMarkConfig examMarkConfig)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.ExamMarkConfigs.Add(examMarkConfig);
                await _context.SaveChangesAsync();

                return Ok(examMarkConfig);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        // DELETE: api/ExamMarkConfigs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamMarkConfig(int id)
        {
            var examMarkConfig = await _context.ExamMarkConfigs.FindAsync(id);
            if (examMarkConfig == null)
            {
                return NotFound();
            }

            _context.ExamMarkConfigs.Remove(examMarkConfig);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExamMarkConfigExists(int id)
        {
            return _context.ExamMarkConfigs.Any(e => e.ExamMarkConfigId == id);
        }
    }
}
